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
    public GameObject levelManager;

    //bool _skipLineTriggered = false;
    public string[] lines;
    private int _INDEX = 0;
    private bool isTyping = false;
    private bool startTyping = false;
    private string name_Text;

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

        name_Text = nameText.text;
        dialogueBox.SetActive(false);
        nameText.text = null;
        StartCoroutine(StartOfScene(2f));
    }

    public void Update()
    {
        if (_INDEX < lines.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !(isTyping) && startTyping)
            {
                StartCoroutine(TypeTextUncapped(lines[_INDEX]));
                _INDEX += 1;
            }
        }
        else 
        {
            StartCoroutine(LoadNewScene(1f));
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
        dialogueBox.gameObject.SetActive(false);
        nameText.text = null;
        dialogueText.text = null;
        yield return new WaitForSeconds (wait);
        levelManager.GetComponent<Transition>().LoadLevel("Hub");
    }

    IEnumerator StartOfScene(float wait)
    {
        yield return new WaitForSeconds(wait);
        dialogueBox.gameObject.SetActive(true);
        startTyping = true;
        StartCoroutine(TypeTextUncapped(lines[_INDEX]));
        _INDEX += 1;
        nameText.text = name_Text;
    }
}
