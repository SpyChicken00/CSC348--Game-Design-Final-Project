using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMovingFish : Fish
{
    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.x += speed * Time.deltaTime; 
        pos = tempPos;
    }
}
