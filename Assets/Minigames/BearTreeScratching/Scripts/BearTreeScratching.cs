using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main controller for tracking bear moves and player input
public class BearTreeScratching : MonoBehaviour
{
    public SpriteRenderer bearRenderer;
    public SpriteRenderer playerRenderer;
    private Animator anim;
    private Animator playerAnim;

    // Controls delay between start of game, between bear moves
    public float startDelay;
    public float moveDelay;

    public GameObject LevelManager;

    // How many moves in the level
    public int movesQuantity;
    public int roundsToWin = 3;

    public GameObject StartUpScratch;
    public GameObject StartDownScratch;
    public GameObject StartLeftScratch;
    public GameObject StartRightScratch;

    public AudioClip winSound;
    public AudioClip loseSound;

    public GameObject mainCamera;

    // stores bear moves, counts how many times a player has moved, stores player input
    // stores start time, stores when the bear finishes moving
    private int[] bearMoves;
    private int playerMoveIndex;
    private int playerMove;
    private float startTime;
    private float bearMovesEndTime;
    private int roundsWon = 0;


    // Controls sequence of bear moves and instantiates vars
    void Start()
    {
        playerRenderer = GameObject.Find("playerBear").GetComponent<SpriteRenderer>();
        //Instantiates vars
        playerMoveIndex = 0;
        anim = GetComponent<Animator>();
        playerAnim = GameObject.Find("playerBear").GetComponent<Animator>();

        mainCamera = GameObject.Find("Main Camera");

        // Sets and executes bear moves
        bearMoves = DecideBearMoves(movesQuantity);
        StartCoroutine(MakeBearMoves());
    }

    // randomly selects bear moves
    int[] DecideBearMoves(int numMoves)
    {
        // saves when the bear moves will end so players cannot input early
        startTime = Time.time;
        bearMovesEndTime = startDelay + ((movesQuantity - 1) * moveDelay);

        // sets moves
        int[] moves = new int[numMoves];
        for (int i = 0; i < numMoves; i++)
            moves[i] = Random.Range(0, 4);

        // outputs moves to the console
        LogMoves(moves);
        
        return moves;
    }

    // executes bear moves while pausing between each one
    private IEnumerator MakeBearMoves()
    {
        // waits before start
        yield return new WaitForSeconds(startDelay);

        // makes moves
        for (int i = 0; i < bearMoves.Length; i++)
        {
            MoveBear(bearMoves[i]);
            yield return new WaitForSeconds(moveDelay);
            anim.speed = 0;
        }

        // Moves camera from bear to player
        yield return new WaitForSeconds(0.5f);
        mainCamera.GetComponent<ZoomCameraStart>().getTimeDuration(1.0f);
        mainCamera.GetComponent<ZoomCameraStart>().bearToPlayer();
    }

    //animates bear in sync with scratch direction
    void MoveBear(int direction)
    {
        //spawn swipe animation and make play in time with bear swipe
        switch (direction)
        {
            case 0:
                Instantiate(StartLeftScratch, new Vector3(-3.4f, -1.9f, 10), Quaternion.Euler(0,0, 90));
                bearRenderer.flipX = false;
                anim.Play("LeftSwipe");
                anim.speed = 0.6f;
                break;
            case 1:
                //Debug.Log("up");
                Instantiate(StartDownScratch, new Vector3(-3.263f, -1.9f, 10), Quaternion.identity);
                bearRenderer.flipX = false;
                anim.Play("DownSwipe");
                anim.speed = 0.6f;
                break;
            case 2:
                Instantiate(StartRightScratch, new Vector3(-3.2f, -1.7f, 10), Quaternion.Euler(0, 0, 90));
                bearRenderer.flipX = true;
                anim.Play("RightSwipe");
                anim.speed = 0.6f;
                break;
            case 3:
                //Debug.Log("down");
                Instantiate(StartUpScratch, new Vector3(-3.263f, -1.80f, 10), Quaternion.identity);
                bearRenderer.flipX = false;
                anim.Play("UpSwipe");
                anim.speed = 0.6f;
                break;
            default:
                break;
        }

        StartCoroutine(StopBearSwipe());
    }

    // Pauses and resets animation speeds when bear is done swiping
    IEnumerator StopBearSwipe()
    {
        yield return new WaitForSeconds(1.0f);
        anim.speed = 0;
        playerAnim.speed = 0;
    }

    // Controls player actions
    void Update()
    {
        // if incorrect key or all moves have been played, do nothing
        if (!Input.GetKeyDown("right") && !Input.GetKeyDown("up") && !Input.GetKeyDown("left") && !Input.GetKeyDown("down") && !Input.GetKeyDown(KeyCode.W) 
            && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D) || playerMoveIndex >= movesQuantity) return;

        // If a player plays while the bear is acting, lose
        // should we have the player lose or just not acknowldge their inputs?
        float tempTime = bearMovesEndTime * 1.55f;
        if(Input.anyKeyDown && Time.time - startTime < tempTime) { Debug.Log("Early Press"); return; }//Lose(); return; }

        // If a key was hit, player acts
        if (Input.anyKeyDown)
        {   
            //anim.loop = true;
            // set playerMove if a direction is hit, returns if other button is hit
            if (Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.D)) {
                playerMove = 0;
                Instantiate(StartLeftScratch, new Vector3(2.25f, -1.9f, 10), Quaternion.Euler(0,0, 90));
                playerRenderer.flipX = false;
                playerAnim.speed = 0.6f;
                playerAnim.Play("LeftSwipe");
                StartCoroutine(StopBearSwipe());
            }
            else if (Input.GetKeyDown("up")|| Input.GetKeyDown(KeyCode.W)) {
                playerMove = 1;
                Instantiate(StartDownScratch, new Vector3(2.35f, -1.9f, 10), Quaternion.identity);
                playerRenderer.flipX = false;
                playerAnim.speed = 0.6f;
                playerAnim.Play("DownSwipe");
                StartCoroutine(StopBearSwipe());
            }
            else if (Input.GetKeyDown("left")|| Input.GetKeyDown(KeyCode.A)) {
                playerMove = 2;
                Instantiate(StartRightScratch, new Vector3(2.5f, -1.7f, 10), Quaternion.Euler(0, 0, 90));
                playerRenderer.flipX = true;     //TODO switch to playerRenderer
                playerAnim.speed = 0.6f;
                playerAnim.Play("RightSwipe");
            
                StartCoroutine(StopBearSwipe());
            }
            else if (Input.GetKeyDown("down")|| Input.GetKeyDown(KeyCode.S)) {
                playerMove = 3;
                Instantiate(StartUpScratch, new Vector3(2.35f, -1.80f, 10), Quaternion.identity);
                playerRenderer.flipX = false;
                playerAnim.speed = 0.6f;
                playerAnim.Play("UpSwipe");
                StartCoroutine(StopBearSwipe());
            }
            else
                return;

            // if incorrect button hit, lose
            if (playerMove != bearMoves[playerMoveIndex])
            {
                Debug.Log("incorrect play: lose");
                Debug.Log("The correct answer was " + bearMoves[playerMoveIndex]);
                StartCoroutine(Lose());
                return;
            }
            // if correctly played and all moves played, win
            else if (playerMoveIndex == movesQuantity - 1)
            {
                roundsWon += 1;
                Debug.Log("Round Won: " + roundsWon);
                if (roundsWon == roundsToWin)
                {
                    anim.speed = 0;
                    StartCoroutine(Win());
                }
                else
                {
                    anim.speed = 0;
                    playerMoveIndex = 0;
                    movesQuantity += 1;
                    bearMoves = DecideBearMoves(movesQuantity);
                    //mainCamera.GetComponent<ZoomCameraStart>().playerToBear();
                    StartCoroutine(MoveCamera());
                    StartCoroutine(MakeBearMoves());
                }
            }
            // gives player feedback for correct
            else
            {
                playerMoveIndex += 1;
                Debug.Log("Correct " + playerMoveIndex);
            }
        }
    }
    
    private IEnumerator MoveCamera()
    {
        //moves camera
        yield return new WaitForSeconds(1.0f);
        mainCamera.GetComponent<ZoomCameraStart>().playerToBear();

        //destroy scratches on screen
        GameObject[] scratches = GameObject.FindGameObjectsWithTag("Scratch");
        foreach (GameObject scratch in scratches)
        {
             Destroy(scratch);
        }
    }


    // Loses the minigame and moves to the next one
    public IEnumerator Lose()
    {
        // prevents additional presses for double lives loss
        playerMoveIndex = movesQuantity;

        // Gives player lose feedback
        Debug.Log("Lose!");
        GetComponent<AudioSource>().PlayOneShot(loseSound);

        // Transitions to next minigame
        yield return new WaitForSeconds(1.0f);
        LevelManager.GetComponent<Transition>().LoseMiniGame(0);
    }

    // Wins the minigame and moves to the next one
    public IEnumerator Win()
    {
        // Sets index to prevent extra presses
        playerMoveIndex += 1;
        Debug.Log("Win!");
        GetComponent<AudioSource>().PlayOneShot(winSound);
        ScoreCounter.score += 1;
        HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.score);
        yield return new WaitForSeconds(1.0f);
        LevelManager.GetComponent<Transition>().LoadRandomGame();
    }

    //outputs moves for debugging
    void LogMoves(int[] moves)
    {
        string output = "";

        for (int i = 0; i < moves.Length; i++) {
            switch (moves[i])
            {
            case 0:
                output += "Right, ";
                break;
            case 1:
                output += "Up, ";
                break;
            case 2:
                output += "Left, ";
                break;
            case 3:
                output += "Down, ";
                anim.speed = 0.6f;
                break;
            }
        }
        Debug.Log(output);
    }
}
