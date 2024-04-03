using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAI : MonoBehaviour
{
    private enum Movement { Up, Down, Wait }
    public enum Side { Left, Right }
    private Movement currentMovement = Movement.Wait;
    private int timeBeforeMove = 0;
    private int updateTime = 180;
    private bool keyDown;

    //private Side defaultSide;
    
    
    [SerializeField]
    public Side currentSide;
    public float movementSpeed = 0.01f;
    public float timeBeforeAttack = 1;

    void Start() {
        //defaultSide = currentSide;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeMove >= updateTime) {
            ChooseRandomMovement();
            ChooseSide();
            timeBeforeMove = 0;
        }
        else {
            timeBeforeMove += 1;
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

        keyDown = Input.GetKey(KeyCode.Space);
       
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
                //currentSide = defaultSide;
                break;
        }
    
    }
    //TODO check for collision with line of sight and player
    private void enterLineOfSight() {
        //if player is in line of sight, start countdown to attack 
    }


    private void OnTriggerStay2D(Collider2D other) {
            Debug.Log("Bear sees player");
            //if player is in line of sight, start countdown to attack 
            timeBeforeAttack -= 1 * Time.deltaTime;
            if (timeBeforeAttack <= 0 && keyDown) {
                //attack player
                Debug.Log("Bear attacks player");
            }

    }

    private void OnTriggerExit2D(Collider2D other) {
            //if player is out of line of sight, stop countdown to attack
            timeBeforeAttack = 3;
        
    }

    //bears move up and down their trees at random intervals
    //be able to adjust speed and frequency of movement for difficulty 
    //bears can switch sides of tree?
    //bears can eat berries?
}
