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
        winSound = GetComponent<AudioSource>().GetComponents<AudioSource>()[0];
        loseSound = GetComponent<AudioSource>().GetComponents<AudioSource>()[1];
    }

    public void WinScreen() {
        Debug.Log("You Win!");
        //play win sound
        winSound.Play();
        levelManager.GetComponent<Transition>().WinMiniGame(2.0f);
    }

    public void LoseScreen() {
        Debug.Log("You Lose!");
        //play lose sound
        loseSound.Play();
        levelManager.GetComponent<Transition>().LoseMiniGame(2.0f);
    }

}
