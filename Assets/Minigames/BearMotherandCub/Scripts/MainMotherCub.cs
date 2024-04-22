using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMotherCub : MonoBehaviour
{
    public GameObject Mother;

    public MainCharacter mainCharacter;
    public int numMamaBear;
    public GameObject startPnt;
    public GameObject endPnt;
    public GameObject LevelManager;
    public bool over = false;
    public AudioClip lossSound;
    public AudioClip winSound;
    
    //public AudioClip loseSound;

    // start and end point singletons
    public static GameObject s;
    public static GameObject e;

    // bounds for where MamaBears will be instantiated
    public Vector2 xBounds = new Vector2(-36, 108);
    public Vector2 yBounds = new Vector2(-20, 60);

    // Possible places to start and end
    public Transform[] startLocations;

    void Start()
    {
        // creates and places the start and end
        s = Instantiate<GameObject>(startPnt);
        e = Instantiate<GameObject>(endPnt);

        int startIndex = Random.Range(0, startLocations.Length);
        int endIndex = Random.Range(0, startLocations.Length);
        while (startIndex == endIndex)

            endIndex = Random.Range(0, startLocations.Length);
        s.transform.position = startLocations[startIndex].position;
        e.transform.position = startLocations[endIndex].position;

        // creates bears
        PopulateBears(numMamaBear, startLocations[startIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player gets close enough to the goal, win
        if (System.Math.Abs(mainCharacter.transform.position.x - e.transform.position.x) < 4 &&
            System.Math.Abs(mainCharacter.transform.position.y - e.transform.position.y) < 4)
        {
            Win();
        }
    }

    // loses minigame and transitions to next
    public void Lose()
    {
        // if win or lose has not been achieved, trigger
        if (!over)
        {
            // prevents multiple lives lost in one game, or winning after losing
            over = true;
            // Play Lose Soynd
            GetComponent<AudioSource>().PlayOneShot(lossSound);
            LevelManager.GetComponent<Transition>().LoseMiniGame(1.0f);
        }
    }

    // wins minigame and transitions to next
    public void Win()
    {
        // if win or lose has not been achieved, trigger
        if(!over)
        {
            // prevents multiple wins or losing after winning
            over = true;
            // Play Lose Soynd
            GetComponent<AudioSource>().PlayOneShot(winSound);
            LevelManager.GetComponent<Transition>().WinMiniGame(1.0f);
        }
    }

    // Creates bear pairs
    public void PopulateBears(int numBears, Transform startPoint)
    {
        for (int i = 0; i < numBears; i++)
        {
            // Create MamaBear within bounds set
            float x = Random.Range(xBounds.x, xBounds.y);
            float y = Random.Range(yBounds.x, yBounds.y);
            Vector3 position = new Vector3(x, y, 0);

            // If not close to start, instantiate
            if (Vector3.SqrMagnitude(position - startPoint.position) > Mathf.Pow(MamaBear.radius * 1.25f, 2))
            {
                GameObject go = Instantiate<GameObject>(Mother);
                go.transform.position = position;
            }
            // Retry if Bear is too close to player start point
            else
                i--;
        }
    }
}
