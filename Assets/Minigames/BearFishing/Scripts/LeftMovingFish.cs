/*
 * Title: LeftMovingFish.cs
 * Lead Programmer: Joshua Hutson
 * Description: Inherits Fish.cs and modifies the Move function so the fish moves towards the left
 * Date: April 23rd 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovingFish : Fish
{

    //Method moves the fish from the right side to the left side of the screen
   public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.x -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
