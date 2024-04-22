using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject character;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = MainMotherCub.e.transform.position - character.transform.position;
        direction.Normalize();
        this.transform.position = character.transform.position + direction;
        transform.up = direction;
    }
}
