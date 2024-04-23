/*
 * File Title: LivesCounter
 * Lead Programmer: Hayes Brown
 * Description: Keeps track of player lives
 * Date: 4/23/24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public static int lives = 3;
    public TextMeshProUGUI uiText;
    public static bool show = true;

    // initializes lives counter
    void Start()
    {
        uiText.text = "Lives: " + lives.ToString("#,0");

        uiText.enabled = show;
    }

    // updates based on number of lives
    void Update()
    {
        uiText.text = "Lives: " + lives.ToString("#,0");
        uiText.enabled = show;
    }
}