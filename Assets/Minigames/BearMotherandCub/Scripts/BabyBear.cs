using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBear : MonoBehaviour
{
    protected float _time;
    protected Rigidbody2D rigid;

    [Header("Inscribed")]
    public float speed = 1f;
    public float radius;
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

    // sets a random direction, or staying stationary
    public void DecideDirection()
    {
        facing = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }
}