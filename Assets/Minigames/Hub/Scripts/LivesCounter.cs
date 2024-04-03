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
    // c

    void Start()
    {
        uiText.text = "Lives: " + lives.ToString("#,0");

        uiText.enabled = show;
    }

    void Update()
    {
        uiText.text = "Lives: " + lives.ToString("#,0");
        uiText.enabled = show;
    }
}