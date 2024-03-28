using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    static public Locator S { get; private set; }  // Singleton property    // a

    [Header("Inscribed")]
    // These fields control the movement of the ship
    public float speed = 30;
    private bool isMoving;
    public GameObject spear;
    public float spearSpeed = 1;

    void Awake()
    {
        if (S == null)
        {
            S = this; // Set the Singleton only if it’s null                  // c
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Locator.S!");
        }

        isMoving = true;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = false;
            ShootHarpoon();
        }
    }

    public void ShootHarpoon()
    {
        GameObject go = Instantiate<GameObject>(spear);
        go.transform.position = new Vector3(0, 3, 0);
        Rigidbody2D rigidB = go.GetComponent<Rigidbody2D>();
        Vector3 dvect = this.transform.position - go.transform.position;
        rigidB.velocity = dvect * spearSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherGO = other.gameObject;                                 
        if (otherGO.name == "Spear")
        {                
            Destroy(otherGO);      // Destroy the Spear
            isMoving = true;       // Let the locator move again
        }

        print(otherGO.gameObject.name);
    }
}
