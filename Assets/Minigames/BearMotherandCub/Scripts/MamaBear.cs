using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MamaBear : MonoBehaviour
{
    protected static Vector2[] directions = new Vector2[] {                  // a
        Vector2.right, Vector2.up, Vector2.left, Vector2.down, Vector2.zero };
    protected float _time;
    protected Rigidbody2D rigid;
    protected BabyBear myBaby;
    protected LineRenderer line;

    [Header("Inscribed")]
    public GameObject babyPrefab;
    public float speed = 0.5f;
    public float radius = 10;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Dynamic")]
    public Vector2 facing;
    public float timeNextDecision = 0;

    public Vector2 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        GenerateBaby();
        DecideDirection();
    }

    void Update()
    {
        // if enough time passes, randomize direction
        if (Time.time >= timeNextDecision)
            DecideDirection();

        // move in the direction faced
        rigid.velocity = facing * speed;
        Vector2 tempPos = pos;
        pos = tempPos;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, myBaby.transform.position);
    }


    // sets a random direction, or staying stationary
    void DecideDirection()
    {
        float sqrdDistance = (pos - myBaby.pos).sqrMagnitude;

        // if twice as far as radius, move towards cub, closer than radius, move away, else move random
        if (sqrdDistance > Mathf.Pow(radius * 2, 2))
        {
            //Debug.Log("GET OVER HERE");
            facing = (myBaby.pos - pos).normalized;
        }
        else if (sqrdDistance < Mathf.Pow(radius, 2))
        {
            //Debug.Log("#NotAHelicopter");
            facing = -(myBaby.pos - pos).normalized;
        }
        else
        {
            //Debug.Log("What is parenting?");
            facing = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }

    // creates a child that is linked to the mom, 1 radius distance away
    private void GenerateBaby()
    {
        // instantiates baby
        GameObject go = Instantiate<GameObject>(babyPrefab);
        myBaby = go.GetComponent<BabyBear>();

        // sets position 1 radius away
        myBaby.DecideDirection();
        float goX = this.transform.position.x + (radius * myBaby.facing.x);
        float goY = this.transform.position.y + (radius * myBaby.facing.y);
        myBaby.transform.position = new Vector3(goX, goY, this.transform.position.z);
    }
}
