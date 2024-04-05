using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreenLocs
    {                                        // a
        onScreen = 0,  // 0000 in binary (zero)
        offRight = 1,  // 0001 in binary
        offLeft = 2,  // 0010 in binary
        offUp = 4,  // 0100 in binary
        offDown = 8   // 1000 in binary
    }
    public enum eType { center, inset, outset };

    [Header("Inscribed")]
    public eType boundsType = eType.center;                                   // a
    public float radius = 3f;
    public bool keepOnScreen = true;
    public float waterLower;
    public float waterUpper;
    public float waterLeft;
    public float waterRight;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;

    public GameObject waterfall;

    void Awake()
    {
        waterLower = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        waterUpper = waterfall.transform.position.y;
        waterLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x + 2;
        waterRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - 2;                         // c
    }

    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset     // b
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;

        // Restrict the X position to waterWidth
        if (pos.x > waterRight + checkRadius)
        {                                  // c
            pos.x = waterRight + checkRadius;
            screenLocs |= eScreenLocs.offRight;
        }
        if (pos.x < waterLeft - checkRadius)
        {                                 // c
            pos.x = waterLeft - checkRadius;                                   // c
            screenLocs |= eScreenLocs.offLeft;
        }

        // Restrict the Y position to camHeight
        if (pos.y > waterUpper + checkRadius)
        {                                 // c
            pos.y = waterUpper + checkRadius;                                   // c
            screenLocs |= eScreenLocs.offUp;
        }
        if (pos.y < waterLower - checkRadius)
        {                                // c
            pos.y = waterLower - checkRadius;                                  // c
            screenLocs |= eScreenLocs.offDown;
        }

        if (keepOnScreen && !isOnScreen)
        {                                  // f
            transform.position = pos;                                         // g
            screenLocs = eScreenLocs.onScreen;
        }
    }

    public bool isOnScreen
    {                                                  // e
        get { return (screenLocs == eScreenLocs.onScreen); }
    }

    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;          // a
        return ((screenLocs & checkLoc) == checkLoc);                     // b
    }
}
