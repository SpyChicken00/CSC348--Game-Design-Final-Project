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
    public AudioClip bearSound;
    public AudioClip babyBearSound;
    public GameObject sprite;
    public float rotationSpeed;

    private Rigidbody2D rigid;
    
    

    // Instantiates main and sets original position
    void Start()
    {
        main = GetComponentInChildren<MainMotherCub>();

        transform.position = MainMotherCub.s.transform.position;
        rigid = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // Pull in information from the Input class
        Vector2 movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();

        transform.Translate(speed * Time.deltaTime * movementDirection, Space.World);

        if(movementDirection != Vector2.zero)
            sprite.transform.up = movementDirection;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bear")) {
            if (collision.gameObject.name == "Baby Bear(Clone)") {
                if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(babyBearSound);
                Debug.Log("EEEEEEEEEEEEEK");
            } else {
                if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(bearSound);
                Debug.Log("AHHHHHH");
            }

            main.Lose();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bear")) {
            Debug.Log("OOOOOOOH");
            if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(bearSound);

            main.Lose();
        }
    }

}
