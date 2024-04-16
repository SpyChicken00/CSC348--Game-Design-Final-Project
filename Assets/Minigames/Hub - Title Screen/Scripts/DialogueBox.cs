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

    //public static event Action OnDialogueStarted;
    //public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public string[] lines;
    private int _INDEX = 0;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(TypeTextUncapped(lines[_INDEX]));
                _INDEX += 1;
            }
        }
        else 
        {
            StartCoroutine(LoadNewScene(2f));
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
    }

    IEnumerator LoadNewScene(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("Hub");
    }

    //public void StartDialogue(string[] dialogue, int startPosition, string name)
    //{
    //    nameText.text = name + "...";
    //    dialogueBox.gameObject.SetActive(true);
    //    StopAllCoroutines();
    //    StartCoroutine(RunDialogue(dialogue, startPosition));
    //}

    //IEnumerator RunDialogue(string[] dialogue, int startPosition)
    //{
    //    skipLineTriggered = false;
    //    OnDialogueStarted?.Invoke();

    //    for (int i = startPosition; i < dialogue.Length; i++)
    //    {
    //        dialogueText.text = dialogue[i];
    //        while (skipLineTriggered == false)
    //        {
    //            // Wait for the current line to be skipped
    //            yield return null;
    //        }
    //        skipLineTriggered = false;
    //    }

    //    OnDialogueEnded?.Invoke();
    //    dialogueBox.gameObject.SetActive(false);
    //}

    //public void SkipLine()
    //{
    //    skipLineTriggered = true;
    //}




}
