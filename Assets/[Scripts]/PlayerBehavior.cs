using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField]
    public Transform _spawn;
    [SerializeField]
    public int _lives = 3;
    [SerializeField]
    public int _level = 1;

    [SerializeField]
    float _walkSpeed = 5;

    [SerializeField]
    float _maxSpeed = 20;

    [SerializeField]
    float _jumpForceAmount = 30;

    [SerializeField]
    Transform _groundPoint;

    [SerializeField]
    LayerMask _groundingLayers;

    Joystick _leftJoystick;
    [SerializeField]
    [Range(0f, 1f)]
    float _tresholdForJump = .3f;

    float _airFactor = .3f;
    public int _coinsCollected = 0;

    bool _isGrounded = false;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        if(GameObject.Find("ScreenController"))
            _leftJoystick = GameObject.Find("LeftJoystick").GetComponent<Joystick>();

    }

    // Update is called once per frame
    void Update()
    {
       IsGrounded();
        //Movement functionality
       Move();
       //Jump fucntionality
       Jump();

        if(!_isGrounded) //OnAir
        {
            if(_rigidbody.velocity.y >= 0)
            {
                _animator.SetInteger("State", (int)AnimationStates.JUMP);
            }
            else if(_rigidbody.velocity.y < 0)
            {
                _animator.SetInteger("State", (int)AnimationStates.FALL);
            }
        }
        else
        {
            _animator.SetInteger("State", (int)AnimationStates.IDLE);
        }

        if (gameObject.transform.position.y <= -10)
        {
            _lives--;
            gameObject.transform.position = _spawn.position;
            if (_lives <= 0) SceneManager.LoadScene("GameOver");

        }
    }

    void Move()
    {
        float leftJoystickInput = 0;
        if(_leftJoystick)
        {
            leftJoystickInput = _leftJoystick.Horizontal;
        }
        float xDirection = (Input.GetAxisRaw("Horizontal")/2) + leftJoystickInput; // if it moves to right it is +1 else if it moves to left it is -1

        //Debug.Log(xDirection);

        Flip(xDirection);

        Vector2 force = Vector2.zero;

        if (xDirection > 0)
        {
             force = Vector2.right * _walkSpeed * Time.deltaTime;

            _rigidbody.AddForce(Vector2.right * _walkSpeed * Time.deltaTime);
        }
        else if (xDirection < 0)
        {
            force = Vector2.left * _walkSpeed * Time.deltaTime;
           
        }
        float maxSpeed = _maxSpeed;

        if(force != Vector2.zero)
        {
            if (!_isGrounded)
            {
                force = force * _airFactor;

                maxSpeed = _airFactor * _maxSpeed;
            }
        }
                

        _rigidbody.AddForce(force);

        _rigidbody.velocity = new Vector2(Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed , maxSpeed), _rigidbody.velocity.y);
    }


    void Jump()
    {
       
        //Check the input 
        float leftJoystickVerticalInput = 0;

        if(_leftJoystick)
        {
            leftJoystickVerticalInput = _leftJoystick.Vertical;
        }

        float _isJumping = (Input.GetAxisRaw("Jump")/2) + leftJoystickVerticalInput;

        if(_isGrounded && _isJumping > _tresholdForJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForceAmount);
        }
    }

    void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_groundPoint.position, .1f, Vector2.down, .1f, _groundingLayers);
        _isGrounded = hit;
    }

    void Flip(float direction)
    {
        if (direction < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction > 0)
            transform.localScale = Vector3.one;
    }

    public void ChangeLevel(int level)
    {
        _level = level;
        switch (level)
        {
            case 1:
                _spawn.position = Vector3.zero;
                break;
            case 2:
                _spawn.position = new Vector3(72, 0, 0);
                break;
            case 3:
                _spawn.position = new Vector3(150, 0, 0);
                break;
            case 4:
                _spawn.position = Vector3.zero;
                SceneManager.LoadScene("Menu");
                break;
            default:
                _spawn.position = Vector3.zero;
                break;
        }

        transform.position = _spawn.position;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(_groundPoint.position,Vector3.down * .1f + _groundPoint.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _coinsCollected++;
        }
    }
}
