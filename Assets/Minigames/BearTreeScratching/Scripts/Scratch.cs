using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    private Animator bearScratch;
    // Start is called before the first frame update
    void Start()
    {
        bearScratch = GetComponent<Animator>();
        bearScratch.speed = 1.1f;
        bearScratch.Play("Tree_Scratch");
        //play scratch sound and animation then stop
        StartCoroutine(StopScratch());
    }

    IEnumerator StopScratch()
    {
        yield return new WaitForSeconds(2.0f);
        bearScratch.speed = 0.0f;
    }

}
