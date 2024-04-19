using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAnimation : MonoBehaviour
{

    public AnimationClip walk;
    Animator anim;
    public GameObject levelManager;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Active");
            StartCoroutine(BeginCutscene(1f));
        }
    }

    IEnumerator BeginCutscene(float wait)
    {
        yield return new WaitForSeconds(wait);
        levelManager.GetComponent<Transition>().LoadRandomGame();
    }
}
