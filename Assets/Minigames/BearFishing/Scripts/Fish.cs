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
    public GameObject InterpPoint;
    protected bool isCaught;
    protected bool shouldfall = false;
    protected bool stopfall = false;
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
        if (!isCaught)
        {
            Move();
            // Check whether this Enemy has gone off the bottom of the screen
            if (!(wtrCheck.isOnScreen))
            {
                Destroy(gameObject);
            }
        }
        else if (System.Math.Abs(InterpPoint.transform.position.x - pos.x) < 0.01 &&
           System.Math.Abs(InterpPoint.transform.position.y - pos.y) < 0.01 && !shouldfall)
        {
            shouldfall = true;
        }
        else
        {
            CaughtMove();
        }

    }

    public virtual void Move(){}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name == "Spear(Clone)")
        {
            Destroy(go);
            isCaught = true;
            drumroll.Play();
            isCatchingFish = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name == "Main Character")
        {
            if (Input.GetKey(KeyCode.Space) && shouldfall)
            {
                this.transform.position = collision.transform.position;
                stopfall = true;
                drumroll.Stop();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name == "Waterfall")
        {
            //stopfall = false;
            //stop drumroll sound
            
            if (secondTime) {
                drumroll.Stop();
                //Destroy(gameObject);
                
                isCatchingFish = false;
                //play one off splash sound
                splash.Play();
                gameObject.transform.position = new Vector3(0, -10, 0);
                
                //need to reanimate locator by teleporting fish offscreen instead of destroying
                //stopfall = true;
            }
            secondTime = true; 
        }

        if (go.name == "Main Character")
        {
            //
        }
    }

    private void CaughtMove()
    {
        if (!shouldfall)
        {
            Vector3 mvmtVtr = InterpPoint.transform.position - this.transform.position;
            pos += 2* mvmtVtr * Time.deltaTime;
        }
        else if(shouldfall && !stopfall)
        { 
            pos -= new Vector3(0, 4, 0) * Time.deltaTime;
        }
    }

    public bool GetIsCatchingFish()
    {
        return isCatchingFish;
    }
}
