using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBranches : MonoBehaviour
{
    private const float MAX_HEIGHT = 1.46f;
    private const float MIN_HEIGHT = -7.2f;
    private float treeLocation;
    
    [SerializeField]
    //public int numOfBranches = 5;
    public int branchNum = 5;
    public GameObject branchPrefabLeft;
    public GameObject branchPrefabRight;  
    

    //TODO - check for overlapping branches
    void Start()
    {
        //branchNum = numOfBranches;
        treeLocation = this.transform.position.x;
        // Instantiate branchNum branches at random positions
        for (int i = 0; i < branchNum; i++)
        {
            // Randomly choose to instantiate Left or Right branch
            if (Random.Range(0, 2) == 0)
            {
                // Instantiate Left at random position and zero rotation.
                Instantiate(branchPrefabLeft, new Vector3(Random.Range(treeLocation - 0.1f, treeLocation + 0.1f), Random.Range(MIN_HEIGHT, MAX_HEIGHT), 1), Quaternion.identity);
            }
            else
            {
                // Instantiate Right at random position and zero rotation.
                Instantiate(branchPrefabRight, new Vector3(Random.Range(treeLocation -0.6f, treeLocation), Random.Range(MIN_HEIGHT, MAX_HEIGHT), 1), Quaternion.identity);
            }
            
        }
       
    }
}




