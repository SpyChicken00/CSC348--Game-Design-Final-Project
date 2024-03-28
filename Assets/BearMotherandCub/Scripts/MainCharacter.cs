using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float speed = 2.5f;
    public GameObject startpoint;
    // Start is called before the first frame update

    private void Awake()
    {
        this.transform.position = startpoint.transform.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pull in information from the Input class
        float hAxis = Input.GetAxis("Horizontal");                            // d
        float vAxis = Input.GetAxis("Vertical");                              // d

        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;
    }
}
