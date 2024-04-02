using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTreeScratching : MonoBehaviour
{
    // Will probably use something like this when we add art
    //public Animator anim;
    //public Rigidbody2D rigid;
    //public SpriteRenderer sRend;
    //public GameObject bear;
    //public GameObject player;

    // Controls delay between start of game, between bear moves
    public float startDelay;
    public float moveDelay;

    public GameObject LevelManager;

    // How many moves in the level
    public int movesQuantity;

    // stores bear moves, counts how many times a player has moved, stores player input
    // stores start time, stores when the bear finishes moving
    private int[] bearMoves;
    private int playerMoveIndex;
    private int playerMove;
    private float startTime;
    private float bearMovesEndTime;

    // Controls sequence of bear moves and instantiates vars
    void Start()
    {
        //Instantiates vars
        startTime = Time.time;
        bearMovesEndTime = startTime + startDelay + ((movesQuantity - 1) * moveDelay);
        playerMoveIndex = 0;

        // Sets and executes bear moves
        bearMoves = DecideBearMoves(movesQuantity);
        StartCoroutine(MakeBearMoves());
    }

    // randomly selects bear moves
    int[] DecideBearMoves(int numMoves)
    {
        int[] moves = new int[numMoves];
        for (int i = 0; i < numMoves; i++)
            moves[i] = Random.Range(0, 4);

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
        }
    }

    // TODO make this animate the bear art when we have art set
    void MoveBear(int direction)
    {
        switch (direction)
        {
            case 0:
                Debug.Log("right");
                break;
            case 1:
                Debug.Log("up");
                break;
            case 2:
                Debug.Log("left");
                break;
            case 3:
                Debug.Log("down");
                break;
            default:
                break;
        }
    }

    // Controls player actions
    void Update()
    {
        // If a player plays while the bear is acting, lose
        if(Input.anyKeyDown && Time.time - startTime < bearMovesEndTime) { Debug.Log("early press: lose"); }

        // If a key was hit, player acts
        if (Input.anyKeyDown)
        {
            // set playerMove if a direction is hit, returns if other button is hit
            if (Input.GetKeyDown("right"))
                playerMove = 0;
            else if (Input.GetKeyDown("up"))
                playerMove = 1;
            else if (Input.GetKeyDown("left"))
                playerMove = 2;
            else if (Input.GetKeyDown("down"))
                playerMove = 3;
            else
                return;

            // if incorrect button hit, lose
            if (playerMove != bearMoves[playerMoveIndex])
            {
                Debug.Log("incorrect play: lose");
                Debug.Log("The correct answer was " + bearMoves[playerMoveIndex]);
            }
            // if correctly played and all moves played, win
            else if (playerMoveIndex == movesQuantity - 1)
            {
                Debug.Log("Win!");
                ScoreCounter.score += 1;
                LevelManager.GetComponent<Transition>().LoadRandomGame();
            }
            // gives player feedback for correct
            else
            {
                playerMoveIndex += 1;
                Debug.Log("Correct " + playerMoveIndex);
            }
        }
    }
}
