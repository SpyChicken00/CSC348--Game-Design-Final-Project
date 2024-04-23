using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Locator : MonoBehaviour
{
    static public Locator S { get; private set; }  // Singleton property    // a

    [Header("Inscribed")]
    public float speed = 30;
    private bool isMoving; //Indicates if the locator should have the ability to move
    public GameObject spear;
    public float spearSpeed = 1;
    private bool haveSpear; //Indicates if a spear is on the screen
    public AudioSource shootSound;

    void Awake()
    {
        if (S == null)
        {
            S = this; // Set the Singleton only if it�s null                  // c
        }
        else
        {
            Debug.LogError("Locator.Awake() - Attempted to assign second Locator.S!");
        }

        //Locator should be moving at the start and should not have fired a spear yet.
        isMoving = true;
        haveSpear = false;
    }

    void Update()
    {
        if (isMoving)
        {
            // Pull in information from the Input class
            float hAxis = Input.GetAxis("Horizontal");                            // d
            float vAxis = Input.GetAxis("Vertical");                              // d

            // Change transform.position based on the axes
            Vector3 pos = transform.position;
            pos.x += hAxis * speed * Time.deltaTime;
            pos.y += vAxis * speed * Time.deltaTime;
            transform.position = pos;
        }

        //If there is no spear on the screen and the player 
        //hits the space bar, fire a spear
        if (Input.GetKeyDown(KeyCode.Space) && !haveSpear)
        {
            isMoving = false;
            //Play shooting sound effect
            shootSound.Play();
            ShootHarpoon();
            haveSpear = true;
        }

    }

    public GameObject ShootHarpoon()
    {
        //Generate the spear at the position of the main character
        GameObject go = Instantiate<GameObject>(spear);
        go.transform.position = new Vector3(0, 3, 0);

        //Set the direction vector and point the spear toward the locator
        Vector3 dvect = this.transform.position - go.transform.position;
        go.transform.rotation = Quaternion.LookRotation(
                       Vector3.forward, // Keep z+ pointing straight into the screen.
                       -dvect        // Point y+ toward the target.
                     ) * Quaternion.Euler(0, 0, -90);

        //Set the velocity of the spear
        Rigidbody2D rigidB = go.GetComponent<Rigidbody2D>();
        rigidB.velocity = dvect * spearSpeed;
        return go;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherGO = other.gameObject;

        //If a spear runs into the locator
        if (otherGO.name == "Spear(Clone)")
        {
            Destroy(otherGO);      // Destroy the Spear
            isMoving = true;       // Let the locator move again
            haveSpear = false;     // Indicate that there is no more spear
        }
    }

    public void reanimate()
    {
        isMoving = true;   // Allow the locator to move again
        haveSpear = false; // Indicate no spear is on the screen
    }
}

