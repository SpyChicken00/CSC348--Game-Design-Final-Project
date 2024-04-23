using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



//Main script for game- controls player movement and branch eating/win conditions
public class Player : MonoBehaviour
{
    public bool keyDown;
    public GameObject LevelManager;
    public int numOfBranches = 3;
    private bool movementDisabled;
    private float timeElapsed = 0;
    private bool gameOver = false;

    [SerializeField]
    public int gameTimer = 30;
    public TextMeshProUGUI timerText;
    public float branchEatingTime = 0.4f;
    public SpriteRenderer playerRenderer;
    
    public float movementSpeed = 0.007f;
    public float minY = -2.7f;
    public float maxY = 1.5f;
    public Animator anim;
    public float animationSpeed = 1.0f;

    public AudioClip shortVictory;
    public AudioClip bearGrowl;
    public AudioClip birdChirp;


    // Initializes vars, waits 2 seconds
    void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<AudioSource>().clip = birdChirp;
        GetComponent<AudioSource>().Play();
        playerRenderer = GetComponent<SpriteRenderer>();
        anim.enabled = false;
        movementDisabled = true;
        StartCoroutine(waitForSeconds(2.0f));

        
    }

    //temporarily disable player movement at the start of the game while zooming
    IEnumerator waitForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds); 
        movementDisabled = false;
        gameTimer = 30;
        timerText.enabled = true;
        timerText.text = gameTimer.ToString();
        StartCoroutine(UpdateTimer());
    }

    //check for player input and win condition
    public void Update()
    {
        anim.speed = animationSpeed;

        if (movementDisabled) {return;}
        //get player input from keyboard
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //move player up
            if (transform.position.y < maxY) {
                transform.position += new Vector3(0, movementSpeed, 0);
                anim.enabled = true;
            }
           
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //move player down
            if (transform.position.y > minY) {
                transform.position += new Vector3(0, -movementSpeed, 0);
                anim.enabled = true;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //teleport to the left
            float y = transform.position.y;
            transform.position = new Vector3(-0.07f, y, 0); //0.0061f
            playerRenderer.flipX = false;
            float y2 = GetComponent<CapsuleCollider2D>().offset.y;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.1544f, y2);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //teleport to the right
            float y = transform.position.y;
            transform.position = new Vector3(0.14f, y, 0);
            playerRenderer.flipX = true;
            float y2 = GetComponent<CapsuleCollider2D>().offset.y;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.05f, y2);
        }

        keyDown = Input.GetKey(KeyCode.Space);

        if (keyDown) {timeElapsed += Time.deltaTime;}
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timeElapsed = 0;        //time based
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            //stop player movement
            anim.enabled = false;
            //Debug.Log("Player stops moving");
        }
        // ends game
        if (numOfBranches <= 0 && !gameOver)
            {
                gameOver = true;
                timerText.enabled = false;
                stopTimer();
                Win();
            }
    
    }

    // stops timer
    public void stopTimer() {
        StopCoroutine(UpdateTimer());
        timerText.text = "0";
        timerText.enabled = false;
    }

    // makes timer tick down
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

            // If timer runs out, lose
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
            timeElapsed = 0;
            }
        }
    }

    public void Win() {
        Debug.Log("All branches have been eaten");
        GetComponent<AudioSource>().clip = birdChirp;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = shortVictory;
        GetComponent<AudioSource>().Play();
        LevelManager.GetComponent<Transition>().WinMiniGame(1.5f);
    }

    public void Lose() {
        Debug.Log("Player loses");
        GetComponent<AudioSource>().clip = birdChirp;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = bearGrowl;
        GetComponent<AudioSource>().Play();
        LevelManager.GetComponent<Transition>().LoseMiniGame(1.0f);
    }

    public void BranchesLeft() {
        numOfBranches -= 1;
        Debug.Log("Branches left: " + numOfBranches);
    }
}