using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    static public string[] GameList { get; private set; } = new string[] { "BearTreeScratching", "BearBrawling" };
    static public int LastGamePlayed = -1;

    public Animator animator;
    public float transitionDelayTime = 1.0f;


    void Awake()
    {
        animator = GameObject.Find("Transition").GetComponent<Animator>();

        // does not show stats if we are in the hub, resets stats in hub
        bool isHub = SceneManager.GetActiveScene().name == "Hub";
        LivesCounter.show = !isHub;
        ScoreCounter.show = !isHub;
        HighScore.show = !isHub;

        if (isHub)
        {
            LivesCounter.lives = 3;
            ScoreCounter.score = 0;
        }
    }

    public void LoseMiniGame(float delay)
    {
        LivesCounter.lives -= 1;
        if (LivesCounter.lives <= 0)
            LoadLevel("Hub");
        else
            DelayLoadRandomGame(delay);
    }

    public void WinMiniGame(float delay)
    {
        ScoreCounter.score += 1;
        DelayLoadRandomGame(delay);
    }

    // Loads random level with a delay
    public void DelayLoadRandomGame(float delay) { StartCoroutine(_DelayLoadRandomGame(delay)); }

    // Loads random game with a delay
    public IEnumerator _DelayLoadRandomGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadRandomGame();
    }

    // Loads a random level from the GameList
    public void LoadRandomGame()
    {
        //select a random game
        int gameIndex = Random.Range(0, Transition.GameList.Length);

        //if the game was just played, try again
        while (gameIndex == LastGamePlayed)
            gameIndex = Random.Range(0, Transition.GameList.Length);

        //set the last game played to the game that will be played, then play the game
        LastGamePlayed = gameIndex;
        LoadLevel(Transition.GameList[gameIndex]);
    }

    // Loads the input level
    public void LoadLevel(string newLevel)
    {
        StartCoroutine(DelayLoadLevel(newLevel));
    }

    // Loads the level with a delay
    IEnumerator DelayLoadLevel(string newLevel)
    {
        animator.SetTrigger("TriggerTransition");
        yield return new WaitForSeconds(transitionDelayTime);
        SceneManager.LoadScene(newLevel);
    }
}