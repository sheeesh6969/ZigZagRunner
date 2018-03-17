using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//huhu arne
//hallo dennis
public class GameManager : MonoBehaviour {

    public bool gameStarted;
    public int score;

    public Text scoreText;
    public Text highscoreText;
	//Was gehhhhht

    private void Awake()
    {
        highscoreText.text = "Best: " + GetHighScore().ToString();
    }

    public void MuteMusic()
    {
        AudioSource audioSource = FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>();
        audioSource.mute = !audioSource.mute;
    }

    public void StartGame()
    {
        gameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
    }

    public void EndGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
            return;
        }

        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                StartGame();
                return;
            }
        }
    }
    
	public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();

        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
