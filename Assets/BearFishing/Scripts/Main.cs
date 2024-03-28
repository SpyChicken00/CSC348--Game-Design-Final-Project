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


    private WaterCheck wtrCheck;
    public GameObject spear;
    public GameObject loc;

    void Awake()
    {
        S = this;
        // Set wtrCheck to reference the WaterCheck component on this 
        // GameObject
        wtrCheck = GetComponent<WaterCheck>();
        //loc.transform.position = Vector3.zero;
        //loc.SetActive(true);

        // Invoke SpawnFish() once (in 2 seconds, based on default values)
        Invoke(nameof(SpawnFish), 1f / enemySpawnPerSecond);                
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
}

