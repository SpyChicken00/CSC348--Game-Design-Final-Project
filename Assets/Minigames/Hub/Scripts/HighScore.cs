using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     // We need this line for uGUI to work.
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI _UI_TEXT;
    static private int _SCORE = 5;

    void Awake()
    {
        // If the PlayerPrefs HighScore already exists, read it
        if (PlayerPrefs.HasKey("HighScore"))
        {                                        // a
            SCORE = PlayerPrefs.GetInt("HighScore");
        }
        // Assign the high score to HighScore
        PlayerPrefs.SetInt("HighScore", SCORE);
    }

    static public int SCORE
    {                                                 // e
        get { return _SCORE; }
        private set
        {                                                         // f
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", value);
        }
    }

    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {                 // h
        if (scoreToTry <= SCORE) return; // If scoreToTry is too low, return
        SCORE = scoreToTry;
    }

    void Update()
    {
        if (_UI_TEXT != null)
            _UI_TEXT.text = "High Score: " + _SCORE.ToString("#,0");
    }

    // The following code allows you to easily reset the PlayerPrefs HighScore
    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;                                           // d

    void OnDrawGizmos()
    {                                                            // e
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
        }
    }

}