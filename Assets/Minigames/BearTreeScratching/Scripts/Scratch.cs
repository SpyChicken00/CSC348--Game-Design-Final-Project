/*
 * File Title: Scratch
 * Lead Programmer: Jace Rettig
 * Description: controls animations for scratches
 * Date: 4/23/24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enables scratch animation and sound for each scratch object
public class Scratch : MonoBehaviour
{
    private Animator bearScratch;
    private SpriteRenderer scratchSprite;
  
    void Start()
    {
        bearScratch = GetComponent<Animator>();
        scratchSprite = GetComponent<SpriteRenderer>();
        bearScratch.speed = 1.1f;
        //adjust scratch sprite renderer order in layer -> decrease by one after a second
        bearScratch.Play("Tree_Scratch");
        //play scratch sound and animation then stop
        StartCoroutine(StopScratch());
    }

    IEnumerator StopScratch()
    {
        yield return new WaitForSeconds(1.0f);
        //make scratch move behind future scratches - change z value
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
