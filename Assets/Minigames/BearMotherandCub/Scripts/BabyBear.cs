using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBear : MonoBehaviour
{
    protected float _time;
    protected Rigidbody2D rigid;

    [Header("Inscribed")]
    public float speed = 1f;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;
    public GameObject sprite;

    [Header("Dynamic")]
    public Vector2 facing;
    public float timeNextDecision = 0;

    // The position of the BabyBear
    public Vector2 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    // Instantiates facing and rigid, then decides where to walk
    void Start()
    {
        facing = Vector2.up;
        rigid = GetComponent<Rigidbody2D>();
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
    }

    // sets a random direction
    public void DecideDirection()
    {
        Vector2 oldFacing = facing;

        facing = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);

        // rotates the sprite to face the way it is going
        float angle = Vector2.SignedAngle(facing, oldFacing);
        sprite.transform.Rotate(0, 0, -angle);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //chooses a random direction to go in
        Vector2 oldFacing = facing;
        facing = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // rotates the sprite to face the way it is going
        float angle = Vector2.SignedAngle(facing, oldFacing);
        sprite.transform.Rotate(0, 0, -angle);

        // sets when a new direction will be chosen
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }
}