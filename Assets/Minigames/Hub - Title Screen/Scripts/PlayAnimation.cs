/*
 * Title: PlayAnimation.cs
 * Lead Programmer: Joshua Hutson
 * Description: Animates the start sign on the start screen when someone clicks the spacebar
 * Date: April 23rd 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAnimation : MonoBehaviour
{
    public AnimationClip walk;
    Animator anim;
    public GameObject levelManager;
    public AudioClip scratch;

    //Get the Animator component on startup 
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Checks for user input on start screen to start the game.
    void Update()
    {
        //Run is mouse is clicked or spacebar is down
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            //Transition from Idle animation to active animation
            anim.SetTrigger("Active");
            StartCoroutine(BeginCutscene(1f));
            
            //Play the associated sound
            GetComponent<AudioSource>().PlayOneShot(scratch);
        }
    }

    //Coroutine to start the dialogue in the cutscene
    IEnumerator BeginCutscene(float wait)
    {
        //Wait to run the animation and then transition to the game itself.
        yield return new WaitForSeconds(wait);
        levelManager.GetComponent<Transition>().LoadRandomGame();
    }
}
