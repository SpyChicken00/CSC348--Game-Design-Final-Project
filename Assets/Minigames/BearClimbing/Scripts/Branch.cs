using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField]
    public GameObject berries;
    public int berryDurability = 3;
    public AudioClip berryEatSound;
    public Player playerObj;
 


    void Start() {
        berries = GameObject.Find("Berries");
        gameObject.SetActive(true);
    }

    public void DecreaseBerryDurability() {   
        StartCoroutine(waitForSeconds(0.5f));
    }

    IEnumerator waitForSeconds(float seconds) {
        //decrease berry durability
        GetComponent<AudioSource>().clip = berryEatSound;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(seconds);
        berryDurability -= 1;
        Debug.Log("Berry Durability: " + berryDurability + "/3");
        if (berryDurability <= 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Branch has been eaten");
            playerObj.BranchesLeft();

        }
        
    }

    
}
