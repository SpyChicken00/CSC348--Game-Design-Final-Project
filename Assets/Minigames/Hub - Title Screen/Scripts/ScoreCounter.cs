/*
 * File Title: ScoreCounter
 * Lead Programmer: Hayes Brown
 * Description: Keeps track of player score
 * Date: 4/23/24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public static int score = 0;
    public TextMeshProUGUI uiText;
    public static bool show;

    // Initializes score counter
    void Start()
    {
        uiText.text = "Score: " + score.ToString("#,0");
        uiText.enabled = show;
    }

    // updates based on changes to score var
    void Update()
    {
        uiText.text = "Score: " + score.ToString("#,0");
        uiText.enabled = show;
    }
}