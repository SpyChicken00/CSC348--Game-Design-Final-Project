using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFish : MonoBehaviour
{
    public GameObject _openMouthBear;
    public GameObject _fishCaughtBear;
    public GameObject InterpPoint;
    private float u;
    private float timeStart;
    private const float TIME_DURATION = 2f;
    public bool isLeft = true;
    private Vector3 startPoint;

    private void Start()
    {
        if (isLeft)
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(Vector3.zero);
            startPoint = this.transform.position;
        }
        else
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            startPoint = this.transform.position;   
        }
        timeStart = Time.time;
    }

    void Update()
    {

        u = (Time.time - timeStart) / TIME_DURATION;
        if (u >= 1) 
        {
            Destroy(this.gameObject);
            //_openMouthBear.SetActive(false);
            //_fishCaughtBear.SetActive(true);
        }
        transform.position = BezierR(u, new Vector3[]{startPoint, InterpPoint.transform.position, _openMouthBear.transform.position });
                                                           
    }

    static public Vector3 BezierR(float u, List<Vector3> pts, int iL = 0, int iR = -1)
    {
        // If iR is the default -1 value, set iR to the last element in pts // b
        if (iR == -1) iR = pts.Count - 1; // c

        // If we are only looking at one element of pts, return it // d
        if (iL == iR) return (pts[iL]);

        // Call BezierR again with all but the leftmost used element of pts
        Vector3 leftVal = BezierR(u, pts, iL, iR - 1); // e
                                                       // And call BezierR again with all but the rightmost used element of pts
        Vector3 rightVal = BezierR(u, pts, iL + 1, iR); // f

        // The result is the Lerp of these two recursive calls to BezierR
        Vector3 res = Vector3.LerpUnclamped(leftVal, rightVal, u); // g
        return (res);
    }

    // This version allows an Array or a series of Vector3 arguments as input
    static public Vector3 BezierR(float u, params Vector3[] arr)
    { // h
        return (BezierR(u, new List<Vector3>(arr)));
    }
}