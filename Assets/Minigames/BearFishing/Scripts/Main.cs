using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // Enables the loading & reloading of scenes

public class Main : MonoBehaviour
{
    static private Main S;                        // A private singleton for Main

    [Header("Inscribed")]
    public GameObject[] prefabFish;               // Array of Fish prefabs
    public float enemySpawnPerSecond = 0.5f;  // # Fish spawned/second
    public float enemyInsetDefault = 1.5f;    // Inset from the sides
    public AudioSource riverSound;


    private WaterCheck wtrCheck;
    public GameObject spear;
    public GameObject loc;
    public GameObject mainCharacter;
    private float _time = 0.0f;  //How much time has already passed
    public float gameTime = 10f; //How long the game lasts in seconds 
    public GameObject[] restingBears;  //List of bear prefabs in resting state
    public GameObject[] MouthOpenBears; //List of bear prefabs with open mouths
    public GameObject[] FishCaughtBears; //List of bear prefabs with fish in mouth
    public GameObject[] FlyingFish;      //List of flying fish prefabs
    
    //These booleans ensure that only one flying fish goes to each bear
    private bool bear1 = true;
    private bool bear2 = true;
    private bool bear3 = true;
    private bool bear4 = true;

    //Indicates if the bears have turned or not
    private bool hasflipped = false;
    public GameObject waterfall;
    public GameObject waterfall2;
    private bool hasWon = false;

    void Awake()
    {
        S = this;
        riverSound = GetComponent<AudioSource>();
        // Set wtrCheck to reference the WaterCheck component on this 
        // GameObject
        wtrCheck = GetComponent<WaterCheck>();
        for(int i = 0; i < MouthOpenBears.Length; i++)
        {
            restingBears[i].SetActive(true);
            MouthOpenBears[i].SetActive(false);
            FishCaughtBears[i].SetActive(false);
        }

        // Invoke SpawnFish() once (in 2 seconds, based on default values)
        Invoke(nameof(SpawnFish), 1f / enemySpawnPerSecond);

        //Start animating the waterfall
        StartCoroutine(MoveWaterfall(0.3f));
    }

    private void Update()
    {
        //Update how much time has passed
        _time += Time.deltaTime;

        //At a fourth of the total time, run first flying fish animation
        if (_time > gameTime / 4 && bear1)
        { 
            //Switch the resting bear for the open mouth bear
            restingBears[0].SetActive(false);
            MouthOpenBears[0].SetActive(true);

            Instantiate<GameObject>(FlyingFish[0]); //Left flying fish
            bear1 = false; //bear has the fish now

            //After 2 seconds, swap the open mouth bear for a fish caught bear
            StartCoroutine(BearSwap(MouthOpenBears[0], FishCaughtBears[0], 2));
        }

        //At a half of the total time, run second flying fish animation
        if (_time > gameTime /2 && bear2)
        {
            //Switch the resting bear for the open mouth bear
            restingBears[1].SetActive(false);
            MouthOpenBears[1].SetActive(true);

            Instantiate<GameObject>(FlyingFish[1]); //Left flying fish
            bear2 = false; // bear 2 has fish now

            //After 2 seconds, swap the open mouth bear for a fish caught bear
            StartCoroutine(BearSwap(MouthOpenBears[1], FishCaughtBears[1], 2));
        }

        //At a three fourths of the total time, run third flying fish animation
        if (_time > (3 *gameTime / 4) && bear3)
        {
            //Switch the resting bear for the open mouth bear
            restingBears[2].SetActive(false);
            MouthOpenBears[2].SetActive(true);

            Instantiate<GameObject>(FlyingFish[2]); //Right flying fish
            bear3 = false; //bear 3 has fish now

            //After 2 seconds, swap the open mouth bear for a fish caught bear
            StartCoroutine(BearSwap(MouthOpenBears[2], FishCaughtBears[2], 2));
        }

        //At the total time, run final flying fish animation
        if (_time > gameTime && bear4)
        {
            //Switch the resting bear for the open mouth bear
            restingBears[3].SetActive(false);
            MouthOpenBears[3].SetActive(true);

            Instantiate<GameObject>(FlyingFish[3]); //Right flying fish
            bear4 = false; //bear 4 has fish now

            //After 2 seconds, swap the open mouth bear for a fish caught bear
            StartCoroutine(BearSwap(MouthOpenBears[3], FishCaughtBears[3], 2));
        }
    }

    public void SpawnFish()
    {
        GameObject go;
          
        // Set the initial position for the spawned Enemy                   
        Vector3 pos = Vector3.zero;
        float yMin = wtrCheck.waterLower;
        float yMax = wtrCheck.waterUpper;
        pos.y = Random.Range(yMin, yMax);

        // Chose either a left moving or right moving fish
        if (UnityEngine.Random.value < 0.5)
        {
            //Right moving fish which means we need to generate on the left side
            go = Instantiate<GameObject>(prefabFish[0]);
            pos.x = wtrCheck.waterLeft;
        }
        else
        {
            //Left moving fish which means we need to generate on the right side
            go = Instantiate<GameObject>(prefabFish[1]);
            pos.x = wtrCheck.waterRight;
        }

        //Set the transform position of the fish.
        go.transform.position = pos;

        // Invoke SpawnFish() again
        Invoke(nameof(SpawnFish), 1f / enemySpawnPerSecond);               
    }

    private void FixedUpdate()
    {
        //Look for all of the fish objects on the screen
        var objects = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject obj in objects)
        {
            // If any of them have fallen off the bottom of the screen, destroy it and reanimate the locator
            // A fish will only fall off the bottom if the player failed to catch it.
            if (obj.GetComponent<WaterCheck>().LocIs(WaterCheck.eScreenLocs.offDown))
            {
                Destroy(obj);
                reanimateLocator();
            }

            //If the player has caught the fish and has not already won, run the win sound and transition
            if (obj.GetComponent<Fish>().GetCaughtStatus() && !hasWon)
            {
                riverSound.GetComponent<AudioSource>().Stop();
                mainCharacter.GetComponent<Fisherman>().WinScreen();
                hasWon = true;
            }
        }

        //If all four bears have caught a fish and have not flipped yet, flip them and start lose screen
        if (!bear4 && !hasflipped)
        {
            hasflipped = true;
            StartCoroutine(BearJudging(2.5f)); //2.3f
        }
    }

    public void reanimateLocator()
    {
        //Call the reanimate method in the locator class to get the locator moving again
        loc.GetComponent<Locator>().reanimate();
    }

    IEnumerator BearSwap(GameObject activeBear, GameObject inactiveBear, float wait)
    {
        //Change one bear prefab for another bear prefab
        yield return new WaitForSeconds(wait);
        inactiveBear.SetActive(true);
        activeBear.SetActive(false);
    }

    IEnumerator BearJudging(float wait)
    {
        yield return new WaitForSeconds(wait);
        loc.SetActive(false); //Stop moving the locator if it is moving

        //Flip all the bears to look at the main character
        foreach (GameObject bear in FishCaughtBears)
        {
            bear.GetComponent<SpriteRenderer>().flipX = !bear.GetComponent<SpriteRenderer>().flipX;
        }       

        //Update to include catching fish as condition before losing
        if(!hasWon)
        {
            riverSound.GetComponent<AudioSource>().Stop();
            mainCharacter.GetComponent<Fisherman>().LoseScreen();
        }
    }

    IEnumerator MoveWaterfall(float wait)
    {
        //Animate the waterfall by rotating the triangle water components in the tile map
        yield return new WaitForSeconds(wait);
        waterfall.SetActive(!waterfall.activeSelf);
        waterfall2.SetActive(!waterfall2.activeSelf);

        //Rotate again
        StartCoroutine(MoveWaterfall(wait));
        
    }
}


