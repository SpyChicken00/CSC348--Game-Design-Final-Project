using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the bear's movement and attack - bears currently move randomly up and down and swap sides randomly
public class BearAI : MonoBehaviour
{
    private enum Movement { Up, Down, Wait }
    public enum Side { Left, Right }
    private Movement currentMovement = Movement.Wait;   
    private float timeBeforeMove = 0;
    public Player playerObj;
    private Animator anim;

    [SerializeField]
    public Side currentSide;
    public int movementUpdateTime = 2;       //in seconds
    public float movementSpeed = 0.007f;
    public float timeBeforeAttack = 0.5f;
    public SpriteRenderer bearRenderer;
    public Sprite bearSprite;
    public float minY = -2.4f;
    public float maxY = 1.5f;
    public int sideSwapFrequency = 10; //how often bear changes side, 0 and 1 are swap, others are nothing, default = 10
    public float visionOffset = 0.23f;
    public float animationSpeed = 1.0f;

    void Start() {
        anim = GetComponent<Animator>();
    }

    //Get player movement and update bear movement
    void Update()
    {
        anim.speed = animationSpeed;
        
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
                if (transform.position.y >= maxY) {currentMovement = Movement.Down;}
                break;
            case Movement.Down:
                if (transform.position.y > minY) {
                    transform.position += new Vector3(0, -movementSpeed, 0);
                    //move vision child small amount too
                    transform.GetChild(0).position -= new Vector3(0, movementSpeed * visionOffset, 0);
                
                }
                //play animation
                if (transform.position.y != minY) {this.GetComponent<Animator>().enabled = true;}
                if (transform.position.y <= minY) {currentMovement = Movement.Up;}
                break;
            case Movement.Wait:
                //stop animation
                this.GetComponent<Animator>().enabled = false;
                break;
        }
       
  }

    //choose random movement direction
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

    //chose random side and flip bear sprite if needed
    private void ChooseSide() {
        // Randomly move to the left or right
        int num = Random.Range(0, sideSwapFrequency);
        switch (num) {
            case 0:
                if (currentSide != Side.Left) {
                    currentSide = Side.Left;
                    //sprite renderer enable xflip
                    bearRenderer.flipX = true;
                }
                break;
            case 1:
                if (currentSide != Side.Right) {
                    currentSide = Side.Right;
                    //sprite renderer disable xflip
                    bearRenderer.flipX = false;
                }  
                break;
            default:
                break;
        }
    
    }
   
    //if player is in line of sight, start countdown to attack
    private void OnTriggerStay2D(Collider2D other) {
            // Debug.Log("Bear sees player");
            timeBeforeAttack -= 1 * Time.deltaTime;
            playerObj.BearAttack(timeBeforeAttack);

    }
    
    //if player is out of line of sight, stop countdown to attack
    private void OnTriggerExit2D(Collider2D other) {
            timeBeforeAttack = 0.5f;
        
    }

}
