using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField]
    public GameObject berries;
    public int berryDurability = 3;
    public AudioClip berryEatSound;


    void Start()
    {
        berries = GameObject.Find("Berries");
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void DecreaseBerryDurability()
    {
        
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
            //GetComponent<AudioSource>().clip = berryEatSound;
            //GetComponent<AudioSource>().Play();
            //yield return new WaitForSeconds(seconds);
            this.gameObject.SetActive(false);
            Debug.Log("Branch has been eaten");

        }
        
    }
    

//TODO - decrease lives when bear sees player eating branch 
//TODO - decrease branch counter when branch is eaten
//TODO - change avatar when "eating" branch - 

    
}
