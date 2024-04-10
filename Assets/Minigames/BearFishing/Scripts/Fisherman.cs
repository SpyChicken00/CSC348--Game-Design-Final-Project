using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fisherman : MonoBehaviour
{
    public GameObject levelManager;

    public void WinScreen() {
        Debug.Log("You Win!");
        levelManager.GetComponent<Transition>().WinMiniGame(1.5f);
    }

    public void LoseScreen() {
        Debug.Log("You Lose!");
        levelManager.GetComponent<Transition>().LoseMiniGame(1.5f);
    }

}
