using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTreeScratching : MonoBehaviour
{
    public SpriteRenderer bearRenderer;
    private Animator anim;

    // Controls delay between start of game, between bear moves
    public float startDelay;
    public float moveDelay;

    public GameObject LevelManager;

    // How many moves in the level
    public int movesQuantity;
    public int roundsToWin = 3;


    //TODO take 3 rounds to win game, each round repeat # goes up by 1
    //TODO sync animations with bear moves



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

        //Instantiates vars
        playerMoveIndex = 0;
        anim = GetComponent<Animator>();

        // Sets and executes bear moves
        bearMoves = DecideBearMoves(movesQuantity);
        StartCoroutine(MakeBearMoves());
    }

    // randomly selects bear moves
    int[] DecideBearMoves(int numMoves)
    {
        startTime = Time.time;
        bearMovesEndTime = startDelay + ((movesQuantity - 1) * moveDelay);

        int[] moves = new int[numMoves];
        for (int i = 0; i < numMoves; i++)
            moves[i] = Random.Range(0, 4);

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
    }

    // TODO make this animate the bear art when we have art set
    void MoveBear(int direction)
    {
        switch (direction)
        {
            case 0:
                //Debug.Log("right");
                //Play right swipe animation 
                bearRenderer.flipX = true;
                anim.Play("RightSwipe");
                anim.speed = 0.6f;
                break;
            case 1:
                //Debug.Log("up");
                anim.Play("UpSwipe");
                anim.speed = 0.6f;
                break;
            case 2:
                //Debug.Log("left");
                bearRenderer.flipX = false;
                anim.Play("LeftSwipe");
                anim.speed = 0.6f;
                break;
            case 3:
                //Debug.Log("down");
                anim.Play("DownSwipe");
                anim.speed = 0.6f;
                break;
            default:
                break;
        }
    }

    // Controls player actions
    void Update()
    {
        // if incorrect key or all moves have been played, do nothing
        if (!Input.GetKeyDown("right") && !Input.GetKeyDown("up") && !Input.GetKeyDown("left") && !Input.GetKeyDown("down") && !Input.GetKeyDown(KeyCode.W) 
            && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D) || playerMoveIndex >= movesQuantity) return;

        // If a player plays while the bear is acting, lose
        // should we have the player lose or just not acknowldge their inputs?
        if(Input.anyKeyDown && Time.time - startTime < bearMovesEndTime) { Debug.Log("Early Press: Lose"); Lose(); return; }

        // If a key was hit, player acts
        if (Input.anyKeyDown)
        {
            // set playerMove if a direction is hit, returns if other button is hit
            if (Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.D))
                playerMove = 0;
            else if (Input.GetKeyDown("up")|| Input.GetKeyDown(KeyCode.W))
                playerMove = 1;
            else if (Input.GetKeyDown("left")|| Input.GetKeyDown(KeyCode.A))
                playerMove = 2;
            else if (Input.GetKeyDown("down")|| Input.GetKeyDown(KeyCode.S))
                playerMove = 3;
            else
                return;

            // if incorrect button hit, lose
            if (playerMove != bearMoves[playerMoveIndex])
            {
                Debug.Log("incorrect play: lose");
                Debug.Log("The correct answer was " + bearMoves[playerMoveIndex]);
                Lose();
                return;
            }
            // if correctly played and all moves played, win
            else if (playerMoveIndex == movesQuantity - 1)
            {
                roundsWon += 1;
                Debug.Log("Round Won: " + roundsWon);
                if (roundsWon == roundsToWin)
                {
                    Win();
                }
                else
                {
                    playerMoveIndex = 0;
                    movesQuantity += 1;
                    bearMoves = DecideBearMoves(movesQuantity);
                    StartCoroutine(MakeBearMoves());
                }
            }
            // gives player feedback for correct
            //TODO give visual/audio feedback for correct answer, show player moving
            else
            {
                playerMoveIndex += 1;
                Debug.Log("Correct " + playerMoveIndex);
            }
        }
    }

    public void Lose()
    {
        // prevents additional presses for double lives loss
        playerMoveIndex = movesQuantity;

        LevelManager.GetComponent<Transition>().LoseMiniGame(0);
    }

    public void Win()
    {
        playerMoveIndex += 1;
        Debug.Log("Win!");
        ScoreCounter.score += 1;
        HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.score);

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
