using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//using UnityEngine.Advertisements; //Unity Service must be enabled to use this!!!

public class GameManager : MonoBehaviour {

    public bool gameStarted;
    public int score;

    public Text scoreText;
    public Text highscoreText;
	public Text levelText;

// HANDY ADS
//	only Android/IOS/Xiaomi
//	public void ShowRewardedAd()
//	{
//		if (Advertisement.IsReady("rewardedVideo"))
//		{
//			var options = new ShowOptions { resultCallback = HandleShowResult };
//			Advertisement.Show("rewardedVideo", options);
//		}
//	}
//
//	private void HandleShowResult(ShowResult result)
//	{
//		switch (result)
//		{
//		case ShowResult.Finished:
//			Debug.Log("The ad was successfully shown.");
//			//
//			// YOUR CODE TO REWARD THE GAMER
//			// Give coins etc.
//			break;
//		case ShowResult.Skipped:
//			Debug.Log("The ad was skipped before reaching the end.");
//			break;
//		case ShowResult.Failed:
//			Debug.LogError("The ad failed to be shown.");
//			break;
//		}
//	}
// HANDY ADS

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

	public void setCharacterLevel(int level)
	{
		levelText.text = "Du bist Level: " + level.ToString ();
	}

	public void SetStoredExperience(int experience)
	{
		PlayerPrefs.SetInt("PlayerExperience", experience);
	}

	public int GetStoredExperience()
	{
		return PlayerPrefs.GetInt("PlayerExperience");
	}
}
