using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaBear : MonoBehaviour
{
    public int numChildren;
    public GameObject baby;
    // Start is called before the first frame update
    void Start()
    {
        GenerateBaby(numChildren);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateBaby(int children)
    {
        for(int i = 0; i < numChildren; i++)
        {
            Instantiate<GameObject>(baby);
        }
    }
}
