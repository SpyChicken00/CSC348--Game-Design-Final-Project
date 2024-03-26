using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;   // The movement speed is 10m/s
    private WaterCheck wtrCheck;

    void Awake()
    {                                                            // c
        wtrCheck = GetComponent<WaterCheck>();
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
        Move();                                                                // b
        // Check whether this Enemy has gone off the bottom of the screen
        if (wtrCheck.LocIs(WaterCheck.eScreenLocs.offDown))
        {             // a
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    { // c
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    //void OnCollisionEnter(Collision coll)
    //{
    //    GameObject otherGO = coll.gameObject;                                  // a
    //    if (otherGO.GetComponent<ProjectileHero>() != null)
    //    {                // b
    //        Destroy(otherGO);      // Destroy the Projectile
    //        Destroy(gameObject);   // Destroy this Enemy GameObject 
    //    }
    //    else
    //    {
    //        Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);  // c
    //    }
    //}
}
