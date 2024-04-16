using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueBox: MonoBehaviour
{
    public static DialogueBox instance;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueBox;

    //bool _skipLineTriggered = false;
    public string[] lines;
    private int _INDEX = 0;
    private bool isTyping = false;

    float charactersPerSecond = 90;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        StartCoroutine(TypeTextUncapped(lines[_INDEX]));
        _INDEX += 1;
    }

    public void Update()
    {
        if (_INDEX < lines.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !(isTyping))
            {
                StartCoroutine(TypeTextUncapped(lines[_INDEX]));
                _INDEX += 1;
            }
        }
        else 
        {
            StartCoroutine(LoadNewScene(2f));
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(LoadNewScene(0.5f));
        }
    }

    //Line of code to make the text appear one character at a time for effect.
    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;
        isTyping = true;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }

        isTyping = false;
    }

    IEnumerator LoadNewScene(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("Hub");
    }
}
