/*
 * File Title: MainCharacter
 * Lead Programmer: Hayes Brown
 * Description: Moves main character and triggers lose conditions
 * Date: 4/23/24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    [Header("Inscribed")]
    public AudioClip bearSound;
    public AudioClip babyBearSound;
    public GameObject sprite;
    public float rotationSpeed;
    public float speed = 2.5f;

    [Header("Instantiated")]
    public MainMotherCub main;

    // Instantiates main and sets original position
    void Start()
    {
        main = GetComponentInChildren<MainMotherCub>();

        transform.position = MainMotherCub.s.transform.position;
    }

    void Update()
    {
        // Pull in information from the Input class
        Vector2 movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();

        // Moves player
        transform.Translate(speed * Time.deltaTime * movementDirection, Space.World);
        //if player is moving, play walking sound
        //if (movementDirection != Vector2.zero && !GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
            

        // Rotates player
        if(movementDirection != Vector2.zero)
            sprite.transform.up = movementDirection;
    }

    // If player hits a baby bear or crosses the line, lose
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Decides which audio to play based on what is hit
        if (collision.CompareTag("Bear")) {
            if (collision.gameObject.name == "Baby Bear(Clone)") {
                if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(babyBearSound);
            } else {
                if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(bearSound);
            }
            // Lose if a bear is hit
            main.Lose();
        }

        if (collision.CompareTag("Finish"))
        {
            main.Win();
        }
    }

    // If a bear is hit, lose
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bear")) {
            if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(bearSound);
            main.Lose();
        }
    }

}
