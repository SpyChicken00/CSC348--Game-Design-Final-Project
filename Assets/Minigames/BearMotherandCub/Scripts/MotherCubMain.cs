using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherCubMain : MonoBehaviour
{
    public GameObject mama;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate<GameObject>(mama);
        go.transform.position = new Vector2(-4, 0);
        GameObject go2 = Instantiate<GameObject>(mama);
        go2.transform.position = new Vector2(4, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
