using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image healthImg;
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private GameObject blowUpEffect;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;


    // Components
    Player player;
    AudioSource audioSource;

    // BlowUp Sound Effect
    [SerializeField] AudioClip blowUpSound;

    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>().GetComponent<Player>();



    }

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
        GameOver();
    }

    private void UpdateScore()
    {
        scoreText.text = player.point.ToString();
    }

    private void UpdateHealth()
    {
        // To be avoid "array boundary" error:
        if (player.health >= 0)
        {
            healthImg.sprite = healthSprites[player.health];
        }
        else
        {
            // Show 0 health image when the player's health is below 0
            healthImg.sprite = healthSprites[0];
        }
    }

    public void BlowUpEffect(Vector3 transformPosition)
    {
        audioSource.PlayOneShot(blowUpSound);
        Instantiate(blowUpEffect, transformPosition, Quaternion.identity);
    }

    private void GameOver()
    {
        if (player.health <= 0)
        {
            gameOverPanel.SetActive(true);
            gameOverScoreText.text = "SCORE: " + player.point;

            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Game");

            }
        }
    }
}
