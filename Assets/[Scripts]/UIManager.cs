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
    [SerializeField] private TextMeshProUGUI levelText;
    private int lives;
    private int coinsCollected;
    private int score;
    private int level;
    void Start()
    {
        lives = player.GetComponent<PlayerBehavior>()._lives;
        coinsCollected = player.GetComponent<PlayerBehavior>()._coinsCollected;
    }
    void Update()
    {
        if (score < (int)player.transform.position.x)
        {
            score = (int)player.transform.position.x;
        }
        coinsCollected = player.GetComponent<PlayerBehavior>()._coinsCollected;
        scoreText.text = $"Score: {score+(coinsCollected*10)}";
        lives = player.GetComponent<PlayerBehavior>()._lives;
        livesText.text =  $"Lives Left: {lives}";

        level = player.GetComponent<PlayerBehavior>()._level;
        levelText.text = $"Level: {level}";
    }
}
