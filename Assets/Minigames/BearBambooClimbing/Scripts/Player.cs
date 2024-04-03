using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject head;
    public bool keyDown;
    private int holdCounter = 0;
    public int holdFrames = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        head = player.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
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

        if (keyDown) {holdCounter++;}
        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdCounter = 0;
        }
        
            //TODO how to decrease durabiliy with brief pause but enable holding button to continuously progress
        

        //check for branch collision and space keydown
        //if so, decrease berry durability, play sound
    }

    
    

    void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("Colliding with branch");
        //if(keyDown && collider.compareTag("Branch"))
        if (collider.gameObject.tag == "Branch")
        {
            if(keyDown && holdCounter > holdFrames)
            {
            //Debug.Log("Space key pressed");
            //decrease berry durability
            collider.gameObject.GetComponent<Branch>().DecreaseBerryDurability();
            holdCounter = 0;
            //GetComponent..gameObject.GetComponent<Branch>().DecreaseBerryDurability();
            //StartCoroutine(waitForSeconds(3));
            //TODO how to decrease durabiliy with brief pause but enable holding button to continuously progress
            }
        }
        






        //allow player to pass through branch
        //Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
        //if (collider.gameObject.tag == "Branch")
        //{
            //Debug.Log("Colliding with branch");
            //head.GetComponent<Head>().isColliding = true;
            //head.GetComponent<Head>().collidingBranch = collider.gameObject;
        //
    }







}
    



