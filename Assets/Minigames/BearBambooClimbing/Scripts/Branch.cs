using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField]
    public GameObject berries;
    public int berryDurability = 3;
    public AudioClip berryEatSound;
    // Start is called before the first frame update
    void Start()
    {
        berries = GameObject.Find("Berries");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DecreaseBerryDurability()
    {
        
        StartCoroutine(waitForSeconds(0.5f));
    }

    IEnumerator waitForSeconds(float seconds) {
    
        Debug.Log("Space key pressed");
        //decrease berry durability
        berryDurability -= 1;
        GetComponent<AudioSource>().clip = berryEatSound;
        GetComponent<AudioSource>().Play();
        if (berryDurability == 0)
        {
            GetComponent<AudioSource>().clip = berryEatSound;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(seconds);
            this.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(seconds);
    }
    


    
}
