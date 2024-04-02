using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBranches : MonoBehaviour
{
    private const float MAX_HEIGHT = 1.46f;
    private const float MIN_HEIGHT = -8.2f;
    private float treeLocation;
    
    [SerializeField]
    int branchNum = 5;
    public GameObject branchPrefabLeft;
    public GameObject branchPrefabRight;  
    

    //TODO - check for overlapping branches
    void Start()
    {
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


//TODO - check for overlapping branches
//TODO - check for branches on different y levels

//TODO - separate script for each branch - berry interaction

//need to take branch prefab and randomly generate n number of branches on a single tree that
    //are within bounds of screen
    //are not overlapping
    //are on different y levels 
    //are on different sides of the tree (left or right)


