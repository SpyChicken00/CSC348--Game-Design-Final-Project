using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//USING CODE FROM INTERPOLATION EXAMPLE - Moves camera between 3 different locations:
//start to bearTree, bearTree to player, and player to bearTree
public class ZoomCameraStart : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform c0;
    public Transform c1;
    public Transform c2;
    public Transform n1;
    public Transform n0;
    public float uMin = 0;
    public float uMax = 1;
    public float timeDuration = 1;
    public bool loopMove = true; // Causes the move to repeat
    public Easing.Type easingType = Easing.Type.linear;
    public float easingMod = 2;
    // Click the checkToStart checkbox to start moving
    public bool checkToStart = true;
    public bool BearToPlayerStart = true;
    public bool PlayerToBearStart = true;
    public AudioClip discoveredAudio;
    
    [Header("Set Dynamically")]
    public Vector3 p01;
    public Color c01;
    public Quaternion r01;
    public Vector3 s01;
    public bool moving = false;
    public float timeStart;
   
    public void bearToPlayer() {
        BearToPlayerStart = true;
    }
    public void playerToBear() {
        PlayerToBearStart = true;
    }
    public void startMove() {
        checkToStart = true;
    }

    public void getTimeDuration(float time) {
        timeDuration = time;

    }
    // Update is called once per frame
    void Update () {
        if (checkToStart) {
            n0 = c0;
            n1 = c1;
            checkToStart = false;
            moving = true;
            timeStart = Time.time;

        }
        if (BearToPlayerStart) {
            n0 = c1;
            n1 = c2;
            BearToPlayerStart = false;
            moving = true;
            timeStart = Time.time;

        }
        if (PlayerToBearStart) {
            n0 = c2;
            n1 = c1;
            PlayerToBearStart = false;
            moving = true;
            timeStart = Time.time;

        }
        if (moving) {
            float u = (Time.time-timeStart)/timeDuration;
            if (u>=1) {
                u=1;
                if (loopMove) {
                    timeStart = Time.time;
                } else {
                    moving = false; // This line is now within the else clause
                }
            }
            // Adjust u to the range from uMin to uMax
            u = (1-u)*uMin + u*uMax;
            // ^ Look familiar? We're using a linear interpolation here too!

            // The Easing.Ease function modifies u to change tweak movement
            u = Easing.Ease(u, easingType, easingMod);
            // This is the standard linear interpolation function
            p01 = (1-u)*n0.position + u*n1.position;
            
            s01 = (1-u)*n0.localScale + u*n1.localScale;
            // Rotations are treated differently because Quaternions are tricky
            r01 = Quaternion.Slerp(n0.rotation, n1.rotation, u);
            // Apply these to this Cube01
            transform.position = p01;
            
            transform.localScale = s01;
            transform.rotation = r01;
        }
    }
  
}
