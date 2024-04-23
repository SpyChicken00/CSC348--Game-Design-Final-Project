/*
 * File Title: ZoomCamera
 * Lead Programmer: Jace Rettig
 * Description: Interpolates camera
 * Date: 4/23/24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//USING CODE FROM INTERPOLATION EXAMPLE
public class ZoomCamera : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform c0;
    public Transform c1;
    public float uMin = 0;
    public float uMax = 1;
    public float timeDuration = 1;
    public bool loopMove = true; // Causes the move to repeat
    public Easing.Type easingType = Easing.Type.linear;
    public float easingMod = 2;
    // Click the checkToStart checkbox to start moving
    public bool checkToStart = true;
    public AudioClip discoveredAudio;
    
    [Header("Set Dynamically")]
    public Vector3 p01;
    public Color c01;
    public Quaternion r01;
    public Vector3 s01;
    public bool moving = false;
    public float timeStart;
   
    // Update is called once per frame
    void Update () {
        if (checkToStart) {
            checkToStart = false;
            moving = true;
            timeStart = Time.time;
            //GetComponent<AudioSource>().clip = discoveredAudio;
            //GetComponent<AudioSource>().Play();

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
            p01 = (1-u)*c0.position + u*c1.position;
            
            s01 = (1-u)*c0.localScale + u*c1.localScale;
            // Rotations are treated differently because Quaternions are tricky
            r01 = Quaternion.Slerp(c0.rotation, c1.rotation, u);
            // Apply these to this Cube01
            transform.position = p01;
            
            transform.localScale = s01;
            transform.rotation = r01;
        }


        //check if the camera is at end location
        if (transform.position == c1.position)
        {
            //load the hub scene
            StartCoroutine(LoadHub());
        }
    }


    IEnumerator LoadHub()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Hub");
    }
    
}
