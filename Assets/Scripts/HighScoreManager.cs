using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    // Constant string for PlayerPrefs key
    private const string HighScoreKey = "HighScore";

    // Property to access high score value
    public int highScore { get; private set; }

    private void Start()
    {
        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHighScore();
            Debug.Log("High score reset.");
        }
    }

    // Method to save the high score
    public void SaveHighScore(int score)
    {
        // Check if current score is greater than stored high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    // Method to load high score from PlayerPrefs
    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            highScore = PlayerPrefs.GetInt(HighScoreKey);
        }

        else
        {
            highScore = 0;
        }
    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.DeleteKey(HighScoreKey);
        Debug.Log("High score reset to 0.");
    }
}
