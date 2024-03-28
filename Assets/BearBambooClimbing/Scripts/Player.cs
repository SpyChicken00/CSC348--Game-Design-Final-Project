using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get player input from keyboard
        if(Input.GetKey(KeyCode.W))
        {
            //move player up
            transform.position += new Vector3(0, 0.01f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //move player down
            transform.position += new Vector3(0, -0.01f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //teleport to the left
            float y = transform.position.y;
            transform.position = new Vector3(-1, y, 0); //0.0061f
        }
        if (Input.GetKey(KeyCode.D))
        {
            //teleport to the right
            float y = transform.position.y;
            transform.position = new Vector3(1, y, 0);
        }
    }
}
