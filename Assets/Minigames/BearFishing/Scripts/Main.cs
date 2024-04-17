using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // Enables the loading & reloading of scenes

public class Main : MonoBehaviour
{
    static private Main S;                        // A private singleton for Main

    [Header("Inscribed")]
    public GameObject[] prefabFish;               // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;  // # Enemies spawned/second
    public float enemyInsetDefault = 1.5f;    // Inset from the sides
    public AudioSource riverSound;


    private WaterCheck wtrCheck;
    public GameObject spear;
    public GameObject loc;
    public GameObject mainCharacter;
    private float _time = 0.0f;
    public float gameTime = 10f; //how long the game lasts in seconds 
    public GameObject[] restingBears;
    public GameObject[] MouthOpenBears;
    public GameObject[] FishCaughtBears;
    public GameObject[] FlyingFish;
    //private GameObject[] fishObjects;
    private bool bear1 = true;
    private bool bear2 = true;
    private bool bear3 = true;
    private bool bear4 = true;
    private bool hasflipped = false;
    public GameObject waterfall;
    public GameObject waterfall2;
    private bool hasWon = false;
    //private bool isCatchingFish = false;

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
        StartCoroutine(MoveWaterfall(0.3f));
    }

    private void Update()
    {
        //Maybe I just need four individual prefabs and I can add them in from there.
        //I need to ensure that the flying fish is only substantiated once.
        //Just some ideas.
        _time += Time.deltaTime;
        if (_time > gameTime / 4 && bear1)
        {
            restingBears[0].SetActive(false);
            MouthOpenBears[0].SetActive(true);
            Instantiate<GameObject>(FlyingFish[0]);
            bear1 = false;
            StartCoroutine(BearSwap(MouthOpenBears[0], FishCaughtBears[0], 2));
        }

        if (_time > gameTime /2 && bear2)
        {
            restingBears[1].SetActive(false);
            MouthOpenBears[1].SetActive(true);
            Instantiate<GameObject>(FlyingFish[1]);
            bear2 = false;
            StartCoroutine(BearSwap(MouthOpenBears[1], FishCaughtBears[1], 2));
        }

        if (_time > (3 *gameTime / 4) && bear3)
        {
            restingBears[2].SetActive(false);
            MouthOpenBears[2].SetActive(true);
            Instantiate<GameObject>(FlyingFish[2]);
            bear3 = false;
            StartCoroutine(BearSwap(MouthOpenBears[2], FishCaughtBears[2], 2));
        }

        if(_time > gameTime && bear4)
        {
            restingBears[3].SetActive(false);
            MouthOpenBears[3].SetActive(true);
            Instantiate<GameObject>(FlyingFish[3]);
            bear4 = false;
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

        // Invoke SpawnEnemy() again
        Invoke(nameof(SpawnFish), 1f / enemySpawnPerSecond);               
    }

    private void FixedUpdate()
    {
        var objects = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<WaterCheck>().LocIs(WaterCheck.eScreenLocs.offDown))
            {
                Destroy(obj);
                reanimateLocator();
            }

            if (obj.GetComponent<Fish>().GetCaughtStatus() && !hasWon)
            {
                riverSound.GetComponent<AudioSource>().Stop();
                mainCharacter.GetComponent<Fisherman>().WinScreen();
                hasWon = true;
            }
        }

        if (!bear4 && !hasflipped)
        {
            hasflipped = true;
            StartCoroutine(BearJudging(2.5f)); //2.3f
        }
    }

    public void reanimateLocator()
    {
        loc.GetComponent<Locator>().reanimate();
    }

    IEnumerator BearSwap(GameObject activeBear, GameObject inactiveBear, float wait)
    {
        yield return new WaitForSeconds(wait);
        inactiveBear.SetActive(true);
        activeBear.SetActive(false);
    }

    IEnumerator BearJudging(float wait)
    {
        yield return new WaitForSeconds(wait);
        loc.SetActive(false);
        foreach (GameObject bear in FishCaughtBears)
        {
            bear.GetComponent<SpriteRenderer>().flipX = !bear.GetComponent<SpriteRenderer>().flipX;
        }

        //find fish objects on screen
       

        //update to include catching fish as condition before losing
        if(!hasWon)
        {
            riverSound.GetComponent<AudioSource>().Stop();
            mainCharacter.GetComponent<Fisherman>().LoseScreen();
        

        }
    }

    IEnumerator MoveWaterfall(float wait)
    {
        yield return new WaitForSeconds(wait);
        waterfall.SetActive(!waterfall.activeSelf);
        waterfall2.SetActive(!waterfall2.activeSelf);
        StartCoroutine(MoveWaterfall(wait));
        
    }
}


