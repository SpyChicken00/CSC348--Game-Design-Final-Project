using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    static public string[] GameList { get; private set; } = new string[] { "BearTreeScratching", "BearBrawling" };

    public Animator animator;
    public float transitionDelayTime = 1.0f;


    void Awake()
    {
        animator = GameObject.Find("Transition").GetComponent<Animator>();
    }

    public void LoadLevel(string newLevel)
    {
        StartCoroutine(DelayLoadLevel(newLevel));
    }

    IEnumerator DelayLoadLevel(string newLevel)
    {
        animator.SetTrigger("TriggerTransition");
        yield return new WaitForSeconds(transitionDelayTime);
        SceneManager.LoadScene(newLevel);
    }
}