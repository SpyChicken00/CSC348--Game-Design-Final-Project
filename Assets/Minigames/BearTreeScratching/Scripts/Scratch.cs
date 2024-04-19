using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    private Animator bearScratch;
    //private float zBuffer;
    private SpriteRenderer scratchSprite;
    // Start is called before the first frame update
    void Start()
    {
        //zBuffer = 0;
        bearScratch = GetComponent<Animator>();
        scratchSprite = GetComponent<SpriteRenderer>();
        bearScratch.speed = 1.1f;
        //this.transform.position = new Vector3(transform.position.x, transform.position.y, 10-zBuffer);
        //adjust scratch sprite renderer order in layer -> decrease by one after a second
        bearScratch.Play("Tree_Scratch");
        //play scratch sound and animation then stop
        StartCoroutine(StopScratch());
    }

    IEnumerator StopScratch()
    {
        yield return new WaitForSeconds(1.0f);
        //make scratch move behind future scratches - change z value
        //change this objects trasnsform z value
        //zBuffer += 0.02f;
        scratchSprite.sortingOrder -= 1;
        yield return new WaitForSeconds(1.0f);
        scratchSprite.sortingOrder -= 1;   
        yield return new WaitForSeconds(1.0f);
        scratchSprite.sortingOrder -= 1; 
        yield return new WaitForSeconds(1.0f);
        scratchSprite.sortingOrder -= 1; 
        yield return new WaitForSeconds(50.0f);
        scratchSprite.sortingOrder -= 1;  
        //yield return new WaitForSeconds(25);
    }
}
