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
    private GameObject head;
    public bool keyDown;
    private float timeElapsed = 0;
    private bool gameOver = false;

    public GameObject LevelManager;
    [SerializeField]
    public int gameTimer = 30;
    public TextMeshProUGUI timerText;
    public float branchEatingTime = 0.4f;
    public int numOfBranches = 5;

    
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        head = player.transform.GetChild(0).gameObject;
        timerText.text = gameTimer.ToString();
        StartCoroutine(UpdateTimer());
    }

    
    void Update()
    {
        //get player input from keyboard
        if(Input.GetKey(KeyCode.W))
        {
            //move player up
            if (transform.position.y < 4) {transform.position += new Vector3(0, 0.015f, 0);}
            //4
        }
        if (Input.GetKey(KeyCode.S))
        {
            //move player down
            if (transform.position.y > -4.3) {transform.position += new Vector3(0, -0.015f, 0);}
            //-4.3
        }
        if (Input.GetKey(KeyCode.A))
        {
            //teleport to the left
            float y = transform.position.y;
            transform.position = new Vector3(-1, y, 0); //0.0061f
        }
        if (Input.GetKey(KeyCode.D))
        {
            //teleport to the right
            float y = transform.position.y;
            transform.position = new Vector3(1, y, 0);
        }

        keyDown = Input.GetKey(KeyCode.Space);

        if (keyDown) {timeElapsed += Time.deltaTime;}
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timeElapsed = 0;        //time based
        }
        
        //if all branches have been eaten, player wins
        if (numOfBranches <= 0 && !gameOver)
            {
                gameOver = true;
                gameTimer = 0;
                timerText.text = "0";
                timerText.enabled = false;
                stopTimer();
                Win();
            }
    }

    public void stopTimer() {
        StopCoroutine(UpdateTimer());
        timerText.text = "0";
        timerText.enabled = false;
        //TODO HOW TO DISABLE TEXT FROM UPDATING?
        
    }

    private IEnumerator UpdateTimer()
    {
        if (gameOver){
            gameTimer = 0;
            timerText.text = "0";
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
        LevelManager.GetComponent<Transition>().WinMiniGame(2.0f);
    }

    public void Lose() {
        Debug.Log("Player loses");
        LevelManager.GetComponent<Transition>().LoseMiniGame(2.0f);
    }

    public void BranchesLeft() {
        numOfBranches -= 1;
        Debug.Log("Branches left: " + numOfBranches);
    }
}