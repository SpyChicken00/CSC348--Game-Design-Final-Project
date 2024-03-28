using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBranches : MonoBehaviour
{
    [SerializeField]
    int branchNum = 5;
    public GameObject branchPrefabLeft;
    public GameObject branchPrefabRight;   

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate branchNum branches at random positions
        for (int i = 0; i < branchNum; i++)
        {
            // Randomly choose to instantiate Left or Right branch
            if (Random.Range(0, 2) == 0)
            {
                // Instantiate Left at random position and zero rotation.
                Instantiate(branchPrefabLeft, new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-5, 5), 0.5f), Quaternion.identity);
            }
            else
            {
                // Instantiate Right at random position and zero rotation.
                Instantiate(branchPrefabRight, new Vector3(Random.Range(-0.6f, 0), Random.Range(-12, 0), 0.5f), Quaternion.identity);
            }
            
        }
       
    }
}

//TODO - suitable range relative to tree rather than worldspace
//TODO - check for overlapping branches
//TODO - check for branches on different y levels

//TODO - separate script for each branch - berry interaction

//need to take branch prefab and randomly generate n number of branches on a single tree that
    //are within bounds of screen
    //are not overlapping
    //are on different y levels 
    //are on different sides of the tree (left or right)


