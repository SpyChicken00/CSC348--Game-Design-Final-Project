using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{
    [Header("Inscribed")]
    private WaterCheck wtrCheck;

    [Header("Dynamic")]
    public float speed = 5f;
    public GameObject InterpPoint;
    protected bool isCaught;
    protected bool shouldfall = false;


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
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("MainCharacter"))
        {
            //this.transform.position = go.transform.position;
            //Invoke("WinScreen()", 3);
        }
        print("collide");
    }

    private void CaughtMove()
    {
        if (!shouldfall)
        {
            Vector3 mvmtVtr = InterpPoint.transform.position - this.transform.position;
            pos += 2* mvmtVtr * Time.deltaTime;
        }
        else
        {
            pos -= new Vector3(0, 4, 0) * Time.deltaTime;
        }
    }

    private void WinScreen()
    {
        SceneManager.LoadScene("BearFishing");
    }
}