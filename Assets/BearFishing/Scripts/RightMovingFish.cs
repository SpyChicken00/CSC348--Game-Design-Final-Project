using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMovingFish : MonoBehaviour
{
    [Header("Inscribed")]
    private WaterCheck wtrCheck;

    [Header("Dynamic")]
    public float speed = 5f;

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
        Move();
        // Check whether this Enemy has gone off the bottom of the screen
        if (!(wtrCheck.isOnScreen))
        {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.x += speed * Time.deltaTime; 
        pos = tempPos;
    }
}
