using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//4-6-24 TODO task
//TODO - change avatar when "eating" branch -
//TODO - check for overlapping branches
//TODO - add sound effects and music

//Bears switching sides should also switch vision or no? 
//bears have more advanced ai for movement?
//maybe more bears?
//allow bears to see whole player body, but player can only use head to eat branches?

public class Player : MonoBehaviour
{
    //private GameObject head;
    public bool keyDown;
    private float timeElapsed = 0;
    private bool gameOver = false;

    public GameObject LevelManager;
    public int numOfBranches = 3;
    private int eatenBranches = 0;
    [SerializeField]
    public int gameTimer = 30;
    public TextMeshProUGUI timerText;
    public float branchEatingTime = 0.4f;
    
    public float movementSpeed = 0.007f;
    public float minY = -2.7f;
    public float maxY = 1.5f;

    public AudioClip shortVictory;
    public AudioClip bearGrowl;

    
    void Start()
    {
        //GameObject player = GameObject.Find("Player");
        //head = player.transform.GetChild(0).gameObject;
        //numOfBranches = 3;
        timerText.text = gameTimer.ToString();
        StartCoroutine(UpdateTimer());
    }

    
    public void Update()
    {
        //get player input from keyboard
        if(Input.GetKey(KeyCode.W))
        {
            //move player up
            if (transform.position.y < maxY) {transform.position += new Vector3(0, movementSpeed, 0);}
            //4
        }
        if (Input.GetKey(KeyCode.S))
        {
            //move player down
            if (transform.position.y > minY) {transform.position += new Vector3(0, -movementSpeed, 0);}
            //-4.3
        }
        if (Input.GetKey(KeyCode.A))
        {
            //teleport to the left
            float y = transform.position.y;
            transform.position = new Vector3(-0.3f, y, 0); //0.0061f
        }
        if (Input.GetKey(KeyCode.D))
        {
            //teleport to the right
            float y = transform.position.y;
            transform.position = new Vector3(0.3f, y, 0);
        }

        keyDown = Input.GetKey(KeyCode.Space);

        if (keyDown) {timeElapsed += Time.deltaTime;}
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timeElapsed = 0;        //time based
        }
        
        //if all branches have been eaten, player wins
        //
        //Debug.Log("numOfBranches: " + numOfBranches);
        //Debug.Log("eaten branches: " + eatenBranches);
        if (numOfBranches <= 0 && !gameOver)
            {
                gameOver = true;
                timerText.enabled = false;
                stopTimer();
                Win();
            }
        
    }

    public void stopTimer() {
        StopCoroutine(UpdateTimer());
        timerText.text = "0";
        timerText.enabled = false;
    }

    private IEnumerator UpdateTimer()
    {
        if (gameOver){
            
            yield break;
            }
        while(gameTimer > 0)
        {
            yield return new WaitForSeconds(1);
            gameTimer -= 1;
            timerText.text = gameTimer.ToString();
            if (gameTimer <= 0)
            {
                Debug.Log("Time's up!");
                Lose();
            }
            
        }
    }

    public void BearAttack(float timeBeforeAttack) {
            Debug.Log("Bear sees player");
            //if player is in line of sight, start countdown to attack 
            //timeBeforeAttack -= 1 * Time.deltaTime;
            if (timeBeforeAttack <= 0 && keyDown && !gameOver) {
                Debug.Log("Bear sees player eating branch; Bear Attacks!");
                gameOver = true;
                stopTimer();
                Lose();
            }
    }
        

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Branch")
        {
            if (keyDown && timeElapsed > branchEatingTime) {               //TIME BASED  
            //decrease berry durability
            collider.gameObject.GetComponent<Branch>().DecreaseBerryDurability();
            //holdCounter = 0;
            timeElapsed = 0;
            }
        }
    }

    public void Win() {
        Debug.Log("All branches have been eaten");
        GetComponent<AudioSource>().clip = shortVictory;
        GetComponent<AudioSource>().Play();
        LevelManager.GetComponent<Transition>().WinMiniGame(1.5f);
    }

    public void Lose() {
        Debug.Log("Player loses");
        GetComponent<AudioSource>().clip = bearGrowl;
        GetComponent<AudioSource>().Play();
        LevelManager.GetComponent<Transition>().LoseMiniGame(1.0f);
    }

    public void BranchesLeft() {
        numOfBranches -= 1;
        eatenBranches += 1;
        Debug.Log("Branches left: " + numOfBranches);
    }
}