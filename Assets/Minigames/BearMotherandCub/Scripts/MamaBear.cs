using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MamaBear : MonoBehaviour
{
    public int numChildren;
    public GameObject baby;
    public float radius;
    public enum Movement{ Left, Right, Up, Down, Stay }
    public Movement[] mvmtChoices;
    public float speed;
    public float _time;
    public float _interval = 2f;
    public Movement choice;

    public Vector3 pos
    {                                                       // a
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBaby(numChildren);
        choice = ChooseDirMove();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        while (_time >= _interval)
        {
            choice = ChooseDirMove();
            _time -= _interval;
        }

        Vector3 tempPos = pos;
        switch (choice)
        {
            case Movement.Left:
                tempPos.x -= speed * Time.deltaTime;
                break;
            case Movement.Right:
                tempPos.x += speed * Time.deltaTime;
                break;
            case Movement.Up:
                tempPos.y += speed * Time.deltaTime;
                break;
            case Movement.Down:
                tempPos.y -= speed * Time.deltaTime;
                break;
            case Movement.Stay:
                break;
        }
        pos = tempPos;
    }

        private void GenerateBaby(int children)
    {
        for(int i = 0; i < numChildren; i++)
        {
            GameObject go = Instantiate<GameObject>(baby);
            go.transform.position = this.transform.position;
        }
    }

    private Movement ChooseDirMove()
    {
        //Choose a direction to move
        return mvmtChoices[Random.Range(0, mvmtChoices.Length)];
    }
}
