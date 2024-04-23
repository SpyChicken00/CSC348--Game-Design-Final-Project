using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Short script for logo transitioning to the main menu
public class LogoTransition : MonoBehaviour
{
    public GameObject levelManager;
    // Start is called before the first frame update
    void Start()
    {
        //levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine(LogoTransitionCoroutine());
    }

    IEnumerator LogoTransitionCoroutine()
    {
        yield return new WaitForSeconds(3);
        levelManager.GetComponent<Transition>().LoadLevel("Opening");
    }
}

