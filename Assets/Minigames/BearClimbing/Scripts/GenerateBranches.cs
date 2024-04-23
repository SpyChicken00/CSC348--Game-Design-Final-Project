/*
 * File Title: GenerateBranches
 * Lead Programmer: Jace Rettig
 * Description: generates branches
 * Date: 4/23/24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generate specified # of branches for each tree
public class GenerateBranches : MonoBehaviour
{
    private float treeLocation;
    
    [SerializeField]
    public int branchNum = 5;
    public GameObject branchPrefabLeft;
    public GameObject branchPrefabRight;  
    //public float MAX_HEIGHT = 1.46f;
    //public float MIN_HEIGHT = -2.4f;
    public float MAX_HEIGHT = 1.97f;
    public float MIN_HEIGHT = -2.4f;
    public float leftRightOffset = 0;
    public float upDownOffset = 0;
    
    // Initializes branches
    void Start()
    {
        treeLocation = this.transform.position.x;
        // Instantiate branchNum branches at random positions
        for (int i = 0; i < branchNum; i++)
        {
            Instantiate(branchPrefabRight, new Vector3(Random.Range(treeLocation -0.5f + leftRightOffset, treeLocation + 0.5f + leftRightOffset), 
            Random.Range(MIN_HEIGHT + upDownOffset, MAX_HEIGHT + upDownOffset), 1), Quaternion.identity); 
        }
       
    }
}




