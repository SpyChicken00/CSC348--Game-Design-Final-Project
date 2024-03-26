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
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public float waterWidth;
    public float waterHeight;
    public GameObject waterfall;

    void Awake()
    {
        waterHeight = Camera.main.ScreenToWorldPoint(Vector3.zero).y - waterfall.transform.position.y;                             // b
        waterWidth = Screen.width;                            // c
    }

    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset     // b
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;

        // Restrict the X position to camWidth
        if (pos.x > waterWidth + checkRadius)
        {                                  // c
            pos.x = waterWidth + checkRadius;
            screenLocs |= eScreenLocs.offRight;
        }
        if (pos.x < -waterWidth - checkRadius)
        {                                 // c
            pos.x = -waterWidth - checkRadius;                                   // c
            screenLocs |= eScreenLocs.offLeft;
        }

        // Restrict the Y position to camHeight
        if (pos.y > waterHeight + checkRadius)
        {                                 // c
            pos.y = waterHeight + checkRadius;                                   // c
            screenLocs |= eScreenLocs.offUp;
        }
        if (pos.y < -waterHeight - checkRadius)
        {                                // c
            pos.y = -waterHeight - checkRadius;                                  // c
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
