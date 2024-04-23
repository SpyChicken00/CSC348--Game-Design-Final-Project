using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fisherman : MonoBehaviour
{
    public GameObject levelManager;

    [SerializeField]
    public AudioSource winSound;
    public AudioSource loseSound;

    public void Awake() {

        //Get the sounds to play when player wins or loses.
        winSound = GetComponent<AudioSource>().GetComponents<AudioSource>()[0];
        loseSound = GetComponent<AudioSource>().GetComponents<AudioSource>()[1];
    }

    public void WinScreen() {
        Debug.Log("You Win!");
        //Play win sound
        winSound.Play();

        //Signal to overall interface that the player won the game
        levelManager.GetComponent<Transition>().WinMiniGame(2.0f);
    }

    public void LoseScreen() {
        Debug.Log("You Lose!");
        //Play lose sound
        loseSound.Play();

        //Signal to overall interface that the player lost the game
        levelManager.GetComponent<Transition>().LoseMiniGame(2.0f);
    }

}
