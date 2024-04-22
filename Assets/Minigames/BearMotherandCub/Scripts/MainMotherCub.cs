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
    
    //public AudioClip loseSound;

    // start and end point singletons
    public GameObject s;
    public GameObject e;

    public Vector2 xBounds = new Vector2(-36, 108);
    public Vector2 yBounds = new Vector2(-20, 60);

    public Transform[] startLocations;

    void Start()
    {
        s = Instantiate<GameObject>(startPnt);
        e = Instantiate<GameObject>(endPnt);

        int startIndex = Random.Range(0, startLocations.Length);
        int endIndex = Random.Range(0, startLocations.Length);
        while (startIndex == endIndex)
            endIndex = Random.Range(0, startLocations.Length);

        s.transform.position = startLocations[startIndex].position;
        e.transform.position = startLocations[endIndex].position;

        PopulateBears(numMamaBear);
    }

    // Update is called once per frame
    void Update()
    {

        if (System.Math.Abs(mainCharacter.transform.position.x - e.transform.position.x) < 0.25 &&
            System.Math.Abs(mainCharacter.transform.position.y - e.transform.position.y) < 0.25)
        {
            mainCharacter.Restart();
        }
    }

    private IEnumerator RestartScene(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("BearMotherandCub");
    }

    public void Lose()
    {
        Debug.Log("Lose");
        //play lose sound
        //GetComponent<AudioSource>().PlayOneShot(loseSound);
    }

    public void PopulateBears(int numBears)
    {
        for (int i = 0; i < numBears; i++)
        {
            float x = Random.Range(xBounds.x, xBounds.y);
            float y = Random.Range(yBounds.x, yBounds.y);

            GameObject go = Instantiate<GameObject>(Mother);
            go.transform.position = new Vector3(x, y, 0);
        }
    }
}
