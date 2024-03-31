using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fisherman : MonoBehaviour
{
    public float speed = 2.5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "LeftMovingFish(Clone)" || collision.gameObject.name == "RightMovingFish(Clone)")
        {
            if (Input.GetKeyDown(KeyCode.Space) && (collision.gameObject.transform.position.y - this.transform.position.y) < 0.1)
            {
                collision.transform.position = this.transform.position;
                Invoke("WinScreen()", 3);
                print("Caught. Yay!");
            }
            print("You Missed");
        }
        print("collided");
    }


}
