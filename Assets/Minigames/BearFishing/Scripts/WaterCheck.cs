/*
 * Title: WaterCheck.cs
 * Lead Programmer: Joshua Hutson
 * Description: Keeps certain objects on the screen and destroys others when they leave the screen.
 * Date: April 23rd 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreenLocs
    {                                        
        onScreen = 0,  // 0000 in binary (zero)
        offRight = 1,  // 0001 in binary
        offLeft = 2,  // 0010 in binary
        offUp = 4,  // 0100 in binary
        offDown = 8   // 1000 in binary
    }
    public enum eType { center, inset, outset };

    [Header("Inscribed")]
    public eType boundsType = eType.center;                                   
    public float radius = 3f;
    public bool keepOnScreen = true;
    public float waterLower;
    public float waterUpper;
    public float waterLeft;
    public float waterRight;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;

    public GameObject waterfall;

    //Initialize the bounds of the water on the screen
    void Awake()
    {   
        waterLower = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        waterUpper = waterfall.transform.position.y;
        waterLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x + 2;
        waterRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - 2;                         
    }

    //After every frame, check where the object is and keep it where it is if need bb.
    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset     
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;

        // Restrict the X position to water Width
        if (pos.x > waterRight + checkRadius)
        {                                 
            pos.x = waterRight + checkRadius;
            screenLocs |= eScreenLocs.offRight;
        }
        if (pos.x < waterLeft - checkRadius)
        {                                 
            pos.x = waterLeft - checkRadius;                                   
            screenLocs |= eScreenLocs.offLeft;
        }

        // Restrict the Y position to water Height
        if (pos.y > waterUpper + checkRadius)
        {                                 
            pos.y = waterUpper + checkRadius;                                   
            screenLocs |= eScreenLocs.offUp;
        }
        if (pos.y < waterLower - checkRadius)
        {                                
            pos.y = waterLower - checkRadius;                                  
            screenLocs |= eScreenLocs.offDown;
        }

        //If we are trying to keep the object in the water, keep it in the water
        if (keepOnScreen && !isOnScreen)
        {                                  
            transform.position = pos;                                         
            screenLocs = eScreenLocs.onScreen;
        }
    }

    //Check if an object is on the screen.
    public bool isOnScreen
    {                                                  
        get { return (screenLocs == eScreenLocs.onScreen); }
    }

    //Check if the object is off in a certain direction
    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;         
        return ((screenLocs & checkLoc) == checkLoc);                     
    }
}
