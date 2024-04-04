using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMotherCub : MonoBehaviour
{
    public BabyBear Cub;
    public MamaBear[] Mother;
    public GameObject startPnt;
    public GameObject endPnt;
    public MainCharacter mainCharacter;
    public int numMamaBear;
    // Start is called before the first frame update
    void Start()
    {
        startPnt.transform.position = new Vector3(-8,0,0);
        endPnt.transform.position =(new Vector3(8,0,0));
    }

    // Update is called once per frame
    void Update()
    {

        if (System.Math.Abs(mainCharacter.transform.position.x - endPnt.transform.position.x) < 0.25 &&
            System.Math.Abs(mainCharacter.transform.position.y - endPnt.transform.position.y) < 0.25)
        {
            mainCharacter.Restart();
        }
    }
    private IEnumerator RestartScene(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("BearMotherandCub");
    }
}
