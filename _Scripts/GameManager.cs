using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text livesText;
    public Text scoreText;
    public bool gameOver = false;
    public GameObject gameOverPanel;
    public Text endScoreText;
    public int highScore;
    public Text highScoreText;
    public Text startScoreText;
    public GameObject titlePanel;
    public GameObject healthPanel;


    private void Start() {
    startScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
}
 
    public void UpdateLives(float lives){
        livesText.text = "Lives: " + lives.ToString();
    }
    
    public void UpdateScore(int score){
            scoreText.text = "Score: " + score.ToString();

    }


    public void  GameOver(int score)
    {
        CalculateEndScore(score);
        gameOverPanel.SetActive(true);
        titlePanel.SetActive(false);
        if(healthPanel.activeInHierarchy){
            healthPanel.SetActive(false);
        }
        gameOver = true;
        Spawner.instance.gameOver = true;
        Camera.main.GetComponent<CameraFollow>().gameOver = true;
        
    }

    private void CalculateEndScore(int score)
    {

        PlayerPrefs.SetInt("score", score);
        if (PlayerPrefs.HasKey("highScore"))
        {

            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
            }

        }
        else
        {
            PlayerPrefs.SetInt("highScore", score);

        }
        endScoreText.text = PlayerPrefs.GetInt("score").ToString();
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
        
        scoreText.enabled = false;
    }

    public void Reload(){
        SceneManager.LoadScene("Game");
    }

    public void Exit(){
        Application.Quit();

    }
}
