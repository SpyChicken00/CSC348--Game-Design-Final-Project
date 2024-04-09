using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAI : MonoBehaviour
{
    private enum Movement { Up, Down, Wait }
    public enum Side { Left, Right }
    private Movement currentMovement = Movement.Wait;
    private float timeBeforeMove = 0;
    public Player playerObj;

    [SerializeField]
    public Side currentSide;
    public int updateTime = 2;       //in seconds
    public float movementSpeed = 0.01f;
    public float timeBeforeAttack = 0.5f;




    void Update()
    {
        if (timeBeforeMove >= updateTime) {
            ChooseRandomMovement();
            ChooseSide();
            timeBeforeMove = 0;
        }
        else {
            timeBeforeMove += 1 * Time.deltaTime;
        }

        switch (currentMovement) {
            case Movement.Up:
                if (transform.position.y < 4) {transform.position += new Vector3(0, movementSpeed, 0);}
                break;
            case Movement.Down:
                if (transform.position.y > -4.3) {transform.position += new Vector3(0, -movementSpeed, 0);}
                break;
            case Movement.Wait:
                break;
        }
       
  }

    private void ChooseRandomMovement() {
        // Randomly move up or down
        int num = Random.Range(0, 3);
        switch (num) {
            case 0:
                currentMovement = Movement.Up;
                break;
            case 1:
                currentMovement = Movement.Down;
                break;
            case 2:
                currentMovement = Movement.Wait;
                break;
        }
    }

    private void ChooseSide() {
        // Randomly move to the left or right
        int num = Random.Range(0, 10);
        switch (num) {
            case 0:
                if (currentSide != Side.Left) {
                    transform.position = new Vector3(transform.position.x-2, transform.position.y, 0);
                    currentSide = Side.Left;
                }
                break;
            case 1:
                if (currentSide != Side.Right) {
                    transform.position = new Vector3(transform.position.x+2, transform.position.y, 0);
                    currentSide = Side.Right;
                }
                break;
            default:
                break;
        }
    
    }
   

    private void OnTriggerStay2D(Collider2D other) {
            // Debug.Log("Bear sees player");
            // //if player is in line of sight, start countdown to attack 
            timeBeforeAttack -= 1 * Time.deltaTime;
            // if (timeBeforeAttack <= 0 && keyDown && !lostGame) {
            //     Debug.Log("Bear sees player eating branch; Bear Attacks!");
            //     lostGame = true;
            //     playerObj.stopTimer();
            //     playerObj.Lose();
            // }
            playerObj.BearAttack(timeBeforeAttack);

    }

    private void OnTriggerExit2D(Collider2D other) {
            //if player is out of line of sight, stop countdown to attack
            timeBeforeAttack = 0.5f;
        
    }

}
