using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueBox: MonoBehaviour
{

    //Code for the dialogue box in the opening scene
    public static DialogueBox instance;

    public TextMeshProUGUI dialogueText; //Text in text box
    public TextMeshProUGUI nameText;  // Name above dialogue box
    public GameObject dialogueBox; //The panel around the dialogue (the box itself)
    public GameObject levelManager;

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

        //Start without any dialogue box or text
        name_Text = nameText.text;
        dialogueBox.SetActive(false);
        nameText.text = null;

        //After two seconds add the dialogue interface into the scene and run it.
        StartCoroutine(StartOfScene(2f));
    }

    public void Update()
    {
        //Run if there are still lines of dialogue left to be seen
        if (_INDEX < lines.Length)
        {
            //If someone hits the space bar and no dialogue is currently generating
            //and we have generated the dialogue box interface, move to the next line of dialogue.
            if (Input.GetKeyDown(KeyCode.Space) && !(isTyping) && startTyping)
            {
                StartCoroutine(TypeTextUncapped(lines[_INDEX]));
                _INDEX += 1;
            }
        }
        else 
        {
            //Once all the dialogue has been generated on screen, transition to the start screen
            StartCoroutine(LoadNewScene(1f));
        }

        //Code to skip the cutscene by pressing the right arrow
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
        isTyping = true; //Ensures we don't start generating multiple lines of dialogue at the same time

        //While the dialogue is not yet fully typed
        while (i < chars.Length)
        {
            //Generate all of the text in Fixed Update style (the letters generate at
            //constant time intervals instead of one letter per frame.
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

        //Signal that it is okay to run the next line of dialogue on spacebar input
        isTyping = false; 
    }

    IEnumerator LoadNewScene(float wait)
    {
        //Wait for a few seconds and then deactivate all text and dialogue boxes
        yield return new WaitForSeconds(wait);
        dialogueBox.gameObject.SetActive(false);
        nameText.text = null;
        dialogueText.text = null;

        //Wait again and then transition to the start screen
        yield return new WaitForSeconds (wait);
        levelManager.GetComponent<Transition>().LoadLevel("Hub");
    }

    IEnumerator StartOfScene(float wait)
    {
        //At the beginning of the scene activate all text and dialogue boxes 
        yield return new WaitForSeconds(wait);
        dialogueBox.gameObject.SetActive(true);
        StartCoroutine(TypeTextUncapped(lines[_INDEX]));
        _INDEX += 1;
        nameText.text = name_Text;
        
        //Signal that the dialogue box has generated and text can now be generated too.
        startTyping = true;
    }
}
