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
    }

    // Loads the input level
    public void LoadLevel(string newLevel)
    {
        StartCoroutine(DelayLoadLevel(newLevel));
    }

    // Loads random game with a delay
    public IEnumerator DelayLoadRandomGame(float delay)
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

    // Loads the level with a delay
    IEnumerator DelayLoadLevel(string newLevel)
    {
        animator.SetTrigger("TriggerTransition");
        yield return new WaitForSeconds(transitionDelayTime);
        SceneManager.LoadScene(newLevel);
    }
}