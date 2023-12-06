using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehavior : MonoBehaviour
{
    public enum HazardType {FallingPlatfrom, HorizontalMovingPlatform, Fake}

    [SerializeField] private Rigidbody2D rb;

    private bool enableFall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enableFall) StartCoroutine(ToggleGravity(1f));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enableFall = true;
        }
    }

    IEnumerator ToggleGravity(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.bodyType = RigidbodyType2D.Dynamic;

    }
}
