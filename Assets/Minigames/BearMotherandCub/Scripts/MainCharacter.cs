using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    // Inscribed
    public float speed = 2.5f;

    // Instantiated
    public MainMotherCub main;

    private Rigidbody2D rigid;

    // Instantiates main and sets original position
    void Start()
    {
        main = GetComponentInChildren<MainMotherCub>();

        transform.position = main.s.transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Pull in information from the Input class
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

         //Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        pos.z = 0;
        transform.position = pos;

    }

    public void Restart()
    {
        this.transform.position = main.s.transform.position;
    }

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
