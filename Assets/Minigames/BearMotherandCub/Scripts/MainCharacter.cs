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

    void Start()
    {
        this.transform.position = startPnt.transform.position;
    }

    void Update()
    {
            // Pull in information from the Input class
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            // Change transform.position based on the axes
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
        Debug.Log("AHHHHHH");
    }

}
