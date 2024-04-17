using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float speed = 2.5f;
    public GameObject startPnt;
    public GameObject endPnt;

    private Rigidbody2D rigid;
    private float movementX;
    private float movementY;

    void Start()
    {
        this.transform.position = startPnt.transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    // This may be teleporting the player, not moving them. Check rollaball
    void Update()
    {
        // Pull in information from the Input class
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

         //Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

    }

    public void Restart()
    {
        this.transform.position = startPnt.transform.position;
    }

    // this does not work, no matter what I do
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bear"))
            Debug.Log("AHHHHHH");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bear"))
            Debug.Log("OOOOOOOH");
    }

}
