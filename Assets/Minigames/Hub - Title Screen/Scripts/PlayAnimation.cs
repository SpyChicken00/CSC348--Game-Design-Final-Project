using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAnimation : MonoBehaviour
{

    //This class animates the start sign on the start screen when someone clicks the spacebar

    public AnimationClip walk;
    Animator anim;
    public GameObject levelManager;
    public AudioClip scratch;

    void Start()
    {
        //Get the Animator component
        anim = GetComponent<Animator>();
    }

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

    IEnumerator BeginCutscene(float wait)
    {
        //Wait to run the animation and then transition to the game itself.
        yield return new WaitForSeconds(wait);
        levelManager.GetComponent<Transition>().LoadRandomGame();
    }
}
