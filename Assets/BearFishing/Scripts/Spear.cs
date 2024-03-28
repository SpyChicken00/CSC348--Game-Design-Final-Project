using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterCheck))]
public class Spear : MonoBehaviour
{
    private WaterCheck wtrCheck;

    void Awake()
    {
        wtrCheck = GetComponent<WaterCheck>();
    }

    void Update()
    {
        if (wtrCheck.LocIs(WaterCheck.eScreenLocs.offDown))
        {          
            Destroy(gameObject);
        }
    }
}


