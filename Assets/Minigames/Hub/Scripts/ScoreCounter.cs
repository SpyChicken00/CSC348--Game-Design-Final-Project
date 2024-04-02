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
    // c

    void Start()
    {
        uiText.text = score.ToString("#,0");
    }

    void Update()
    {
        uiText.text = score.ToString("#,0");
    }
}