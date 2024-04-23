using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{
    [Header("Inscribed")]
    private WaterCheck wtrCheck;
    public AudioSource drumroll;
    public AudioSource splash;

    [Header("Dynamic")]
    public float speed = 5f;
    public GameObject InterpPoint; //Used for Bezier Curves and fish dropping
    protected bool isCaught; //Indicates if this fish has been hit with a spear
    protected bool shouldfall = false; //Indicates if the fish is over the player's head and should start falling
    protected bool stopfall = false;  //Indicates if the player has caught the fish in their mouth
    private bool secondTime = false;
    private bool isCatchingFish = false;
    


    void Awake()
    {                                                            // c
        wtrCheck = GetComponent<WaterCheck>();
        drumroll = GetComponent<AudioSource>().GetComponents<AudioSource>()[0];
        splash = GetComponent<AudioSource>().GetComponents<AudioSource>()[1];
        isCaught = false;
    }

    public bool GetCaughtStatus()
    {
        //Indicates whether the player has caught the fish in their mouth.
        return stopfall;
    }

    // This is a Property: A method that acts like a field
    public Vector3 pos
    {                                                       // a
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {
        //Movement for fish that are swimming in the water
        if (!isCaught)
        {
            //Defined by children classes
            Move();

            // Check whether this fish has gone off the screen
            if (!(wtrCheck.isOnScreen))
            {
                Destroy(gameObject);
            }
        }

        //If the fish is within 0.01 units of the interpolation point, it should start falling on the player
        else if (System.Math.Abs(InterpPoint.transform.position.x - pos.x) < 0.01 &&
           System.Math.Abs(InterpPoint.transform.position.y - pos.y) < 0.01 && !shouldfall)
        {
            shouldfall = true;
        }

        // If the fish has been hit with a spear, it should move linearly from its current position to 
        // the interpolation point
        else
        {
            CaughtMove();
        }

    }

    public virtual void Move(){} //Defined in children classes

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        //If the fish is hit with the spear
        if (go.name == "Spear(Clone)")
        {
            Destroy(go); //Destroy the spear
            isCaught = true; //Signal that the fish is caught
            drumroll.Play(); //Play a drumroll
            isCatchingFish = true; //Indicate that the fish is in the process of being caught
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        //While the fish is over the main character's face
        if (go.name == "Main Character")
        {
            //If player gives the correct input
            if (Input.GetKey(KeyCode.Space) && shouldfall)
            {
                //Set position fish is caught/stops at
                this.transform.position = new Vector3(-0.05f, 3.415f, 0);
                stopfall = true;
                drumroll.Stop();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        //If the fish collides with the waterfall
        if (go.name == "Waterfall")
        {
            //Ensure that it's the second time the fish crosses the waterfall
            //It crosses the first time on the way up. The second time means the player missed.
            if (secondTime) {
                drumroll.Stop();               
                isCatchingFish = false;

                //Play one off splash sound
                splash.Play();

                //Teleport the fish offscreen so it's destroyed and the locator is reactivated
                gameObject.transform.position = new Vector3(0, -10, 0);
            }
            secondTime = true; 
        }
    }

    private void CaughtMove()
    {
        //If the fish is on the way up
        if (!shouldfall)
        {
            //Move torwards the interpolation point
            Vector3 mvmtVtr = InterpPoint.transform.position - this.transform.position;
            pos += 2 * mvmtVtr * Time.deltaTime;
        }

        //If the fish is on the way down
        else if(shouldfall && !stopfall)
        { 
            //Make it fall straight down
            pos -= new Vector3(0, 4, 0) * Time.deltaTime;
        }
    }

    public bool GetIsCatchingFish()
    {
        //Indicates if the player has speared a fish and is trying to catch it.
        return isCatchingFish;
    }
}
