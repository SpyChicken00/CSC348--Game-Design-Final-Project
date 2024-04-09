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
    public int movementUpdateTime = 2;       //in seconds
    public float movementSpeed = 0.01f;
    public float timeBeforeAttack = 0.5f;
    public SpriteRenderer bearRenderer;
    public Sprite bearSprite;
    public float minY = -2.4f;
    public float maxY = 1.5f;
    public int sideSwapFrequency = 10; //how often bear changes side, 0 and 1 are swap, others are nothing, default = 10
    public float visionOffset = 0.23f;
    //public Animation bearAnimation;




    void Update()
    {

        bearRenderer.sprite = bearSprite;
        //GetComponent<bearAnimation> = bearAnimation;
        

        if (timeBeforeMove >= movementUpdateTime) {
            ChooseRandomMovement();
            ChooseSide();
            timeBeforeMove = 0;
        }
        else {
            timeBeforeMove += 1 * Time.deltaTime;
        }

        switch (currentMovement) {
            case Movement.Up:
                if (transform.position.y < maxY) {
                    transform.position += new Vector3(0, movementSpeed, 0); 
                    //move vision child small amount too
                    transform.GetChild(0).position += new Vector3(0, movementSpeed * visionOffset, 0);
                
                }
                //play animation
                if (transform.position.y != maxY) {this.GetComponent<Animator>().enabled = true;}
                if (transform.position.y == maxY) {currentMovement = Movement.Wait;}
                break;
            case Movement.Down:
                if (transform.position.y > minY) {
                    transform.position += new Vector3(0, -movementSpeed, 0);
                    //move vision child small amount too
                    transform.GetChild(0).position -= new Vector3(0, movementSpeed * visionOffset, 0);
                
                }
                //play animation
                if (transform.position.y != minY) {this.GetComponent<Animator>().enabled = true;}
                if (transform.position.y == minY) {currentMovement = Movement.Wait;}
                break;
            case Movement.Wait:
                //stop animation
                this.GetComponent<Animator>().enabled = false;
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
        int num = Random.Range(0, sideSwapFrequency);
        switch (num) {
            case 0:
                if (currentSide != Side.Left) {
                    //transform.position = new Vector3(transform.position.x-2, transform.position.y, 0);
                    currentSide = Side.Left;
                    //sprite renderer enable xflip
                    bearRenderer.flipX = true;
                    //move vision child small amount too


                    //get current object name
                    // if (GameObject.Find("bearLeft")) {
                    //     transform.GetChild(0).position = new Vector3(transform.position.x - 10, transform.GetChild(0).position.y, 0.39f);
                    // } else if (GameObject.Find("bearRight")){
                    //     //transform.GetChild(0).position = new Vector3(3., transform.GetChild(0).position.y, 0.39f);           //LeftBear facing Right
                    // }


                    //TODO figure out how to flip the vision or disable when flipped?


                    //change vision child position
                    //check if bear has child called visionRight
                    // if (this.gameObject.transform.GetChild(0).name == "visionRight") {
                    //     transform.GetChild(0).position = new Vector3(5.03f, transform.GetChild(0).position.y, 0.39f);
                    // }
                    // else {
                    //     transform.GetChild(0).position = new Vector3(3.82f, transform.GetChild(0).position.y, 0.39f);           //LeftBear facing Right
                    // } 
                    //  transform.GetChild(0).position = new Vector3(3.82f, transform.GetChild(0).position.y, 0.39f);           //LeftBear facing Right
                }
                break;
            case 1:
                if (currentSide != Side.Right) {
                    //transform.position = new Vector3(transform.position.x+2, transform.position.y, 0);
                    currentSide = Side.Right;
                    //sprite renderer disable xflip
                    bearRenderer.flipX = false;
                    // if (GameObject.Find("bearLeft")) {
                    //     transform.GetChild(0).position = new Vector3(-7.18f, transform.GetChild(0).position.y, 0.39f);
                    // }
                    //transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x - 10, transform.GetChild(0).position.y, 0);
                    // if (this.gameObject.transform.GetChild(0).name == "visionRight") {
                    //     transform.GetChild(0).position = new Vector3(-3.76f, transform.GetChild(0).position.y, 0.39f);
                    // } else {
                    //    
                    //transform.GetChild(0).position = new Vector3(-12.0f, transform.GetChild(0).position.y, 0.39f);          //LeftBear facing Left 
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
