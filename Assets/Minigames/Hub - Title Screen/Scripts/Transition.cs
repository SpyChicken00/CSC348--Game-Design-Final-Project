using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    static public string[] GameList { get; private set; } = new string[] { "BearTreeScratching", "BearBrawling", "BearClimbing", "BearFishing", "BearMotherandCub"};
    static public string[] GameNamesInOrder { get; private set; } = new string[] {"BearFishing", "BearClimbing", "BearTreeScratching", "BearBrawling", "BearMotherandCub"};
    static public int LastGamePlayed = -1;
    public enum GameMode{Random, InOrder}

    public GameMode gameMode = GameMode.Random;
    public Animator animator;
    public float transitionDelayTime = 1.0f;
    private int gameIndex;


    void Awake()
    {
        if (gameMode == GameMode.InOrder) gameIndex = -1;

        // initializes transition animator
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

    // loses a life and checks if the run is over
    public void LoseMiniGame(float delay)
    {
        LivesCounter.lives -= 1;

        // If all lives lost, lose
        if (LivesCounter.lives <= 0)
            SceneManager.LoadScene("Discovered");
        // If still have lives, go to random game
        else
            DelayLoadRandomGame(delay);
    }

    // Increments score, loads random game
    public void WinMiniGame(float delay)
    {
        ScoreCounter.score += 1;
        HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.score);
        DelayLoadRandomGame(delay);
    }

    // Loads random level with a delay
    public void DelayLoadRandomGame(float delay) { StartCoroutine(_DelayLoadRandomGame(delay)); }

    // Loads random game with a delay
    public IEnumerator _DelayLoadRandomGame(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (gameMode == GameMode.Random)
            LoadRandomGame();       //Default Mode to load a random game 
        else if (gameMode == GameMode.InOrder)
            LoadMiniGamesInOrder(); //Special Mode for Demo to showcase all games in order        
    }

    // Loads a random level from the GameList
    public void LoadRandomGame()
    {
        //select a random game
        gameIndex = Random.Range(0, Transition.GameList.Length);

        //if the game was just played, try again
        while (gameIndex == LastGamePlayed)
            gameIndex = Random.Range(0, Transition.GameList.Length);

        LoadLevel(Transition.GameList[gameIndex]);
    }

    //loads the games in order
    public void LoadMiniGamesInOrder() {
        Debug.Log("Loading Mini Games in Order");
        Debug.Log("Game Index: " + gameIndex);
        if (gameIndex == Transition.GameList.Length - 1)
            gameIndex = 0;
        else
            gameIndex += 1;

        LoadLevel(Transition.GameNamesInOrder[gameIndex]);

    }

    // Loads the input level
    public void LoadLevel(string newLevel)
    {
        //set the last game played to the game that will be played, then play the game
        if (gameMode != GameMode.InOrder) {LastGamePlayed = gameIndex;}
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