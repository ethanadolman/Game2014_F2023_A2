using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int lives;
    private int score;

    void Start()
    {
        lives = player.GetComponent<PlayerBehavior>()._lives;
    }
    void Update()
    {
        if (score < (int)player.transform.position.x)
        {
            score = (int)player.transform.position.x;
            scoreText.text =  $"Score: {score}";
            
        }
        lives = player.GetComponent<PlayerBehavior>()._lives;
        livesText.text =  $"Lives Left: {lives}";
    }
}
