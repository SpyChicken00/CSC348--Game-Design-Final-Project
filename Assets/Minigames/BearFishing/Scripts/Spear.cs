/*
 * Title: Spear.cs
 * Lead Programmer: Joshua Hutson
 * Description: Ensures the Spear component is destroyed if it falls off the screen
 * Date: April 23rd 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterCheck))]
public class Spear : MonoBehaviour
{
    private WaterCheck wtrCheck;

    //Get the water check component on start up.
    void Awake()
    {
        wtrCheck = GetComponent<WaterCheck>();
    }

    //While this never fires, in case the spear ever misses the locator, it will be destroyed
    //when it flies off the screen
    void Update()
    {
        if (wtrCheck.LocIs(WaterCheck.eScreenLocs.offDown))
        {
            Destroy(gameObject);
        }
    }
}


