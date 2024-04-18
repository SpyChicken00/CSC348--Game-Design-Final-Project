using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    private Animator bearScratch;
    private int zBuffer;
    // Start is called before the first frame update
    void Start()
    {
        zBuffer = 5;
        bearScratch = GetComponent<Animator>();
        bearScratch.speed = 1.1f;
        bearScratch.Play("Tree_Scratch");
        //play scratch sound and animation then stop
        StartCoroutine(StopScratch());
    }

    IEnumerator StopScratch()
    {
        yield return new WaitForSeconds(1.0f);
        //make scratch move behind future scratches - change z value
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y-zBuffer);
        zBuffer += 1;
    }

}
