using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MamaBear : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected BabyBear myBaby;
    protected LineRenderer line;
    protected EdgeCollider2D lineColl;

    [Header("Inscribed")]
    public GameObject babyPrefab;
    public float speed = 0.5f;
    public float radius = 10;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;
    public GameObject sprite;

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
        lineColl = GetComponent<EdgeCollider2D>();
        GenerateBaby();
        facing = Vector2.up;
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
        List<Vector2> collPoints = new List<Vector2>();
        collPoints.Add(new Vector2(0, 0));
        collPoints.Add(myBaby.pos - pos);
        lineColl.SetPoints(collPoints);
    }


    // sets a random direction, or staying stationary
    void DecideDirection()
    {
        Vector2 oldFacing = facing;

        float sqrdDistance = (pos - myBaby.pos).sqrMagnitude;

        // if twice as far as radius, move towards cub, closer than radius, move away, else move random
        if (sqrdDistance > Mathf.Pow(radius * 2, 2))
            facing = (myBaby.pos - pos).normalized;
        else if (sqrdDistance < Mathf.Pow(radius, 2))
            facing = -(myBaby.pos - pos).normalized;
        else
            facing = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // rotates the sprite to face the way it is going
        float angle = Vector2.SignedAngle(facing, oldFacing);
        sprite.transform.Rotate(0, 0, -angle);

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        //chooses a random direction to go in
        Vector2 oldFacing = facing;
        facing = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // rotates the sprite to face the way it is going
        float angle = Vector2.SignedAngle(facing, oldFacing);
        sprite.transform.Rotate(0, 0, -angle);

        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }
}
