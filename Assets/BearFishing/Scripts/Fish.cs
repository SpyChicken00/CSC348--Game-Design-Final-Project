using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Inscribed")]
    private WaterCheck wtrCheck;

    [Header("Dynamic")]
    public float speed = 5f;
    public GameObject InterpPoint;
    protected bool isCaught;


    void Awake()
    {                                                            // c
        wtrCheck = GetComponent<WaterCheck>();
        isCaught = false;
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
            isCaught=true;
        }
    }

    private void CaughtMove()
    {
        Vector3 mvmtVtr = InterpPoint.transform.position - this.transform.position;
        pos += mvmtVtr * Time.deltaTime;
    }
}
