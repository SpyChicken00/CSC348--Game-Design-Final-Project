using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject character;

    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        // Sets the direction from the player to the goal
        direction = MainMotherCub.e.transform.position - character.transform.position;
        direction.Normalize();

        // puts the arrow pointing towards the goal
        this.transform.position = character.transform.position + direction;
        transform.up = direction;
    }
}