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
    // c

    void Start()
    {
        uiText.text = "Score: " + score.ToString("#,0");
        uiText.enabled = show;
    }

    void Update()
    {
        uiText.text = "Score: " + score.ToString("#,0");
        uiText.enabled = show;
    }
}