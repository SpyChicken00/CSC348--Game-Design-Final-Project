using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    static public Locator S { get; private set; }  // Singleton property    // a

    [Header("Inscribed")]
    // These fields control the movement of the ship
    public float speed = 30;

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
    }

    void Update()
    {
        // Pull in information from the Input class
        float hAxis = Input.GetAxis("Horizontal");                            // d
        float vAxis = Input.GetAxis("Vertical");                              // d

        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //FireHarpoon();
        }
    }

    //void FireHarpoon()
    //{
    //    GameObject go = Instantiate<GameObject> spear;
    //     = new Vector3(0,3,0);

  
    //}
}
