using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BearRockPaperScissors : MonoBehaviour
{
    public Image AIChoice; 
    public BrawlChoice[] Choices;
    public Sprite Rock, Paper, Scissors;
    public TextMeshProUGUI Result;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winCounterText;
    public Difficulty difficulty = Difficulty.Easy;
    public int correctToWin = 3;
    public GameObject LevelManager;

    public AudioClip brawlStartClip;
    public AudioClip  brawlLoopClip120;
    public AudioClip  brawlLoopClip180;
    public AudioClip  brawlLoopClip240;
    public AudioClip  brawlLoopClip300;
    public AudioClip  brawlMistakeClip;
    public AudioClip  brawlDiscoveredClip;
    public AudioClip brawlVictoryClip;

    public enum BrawlChoice{Rock,Paper,Scissors,nullChoice}
    public enum BrawlMusicClip {brawlStartClip,brawlLoopClip120,brawlLoopClip180,brawlLoopClip240,brawlLoopClip300,
                                brawlMistakeClip,brawlDiscoveredClip,brawlVictoryClip}
    public enum Difficulty{Easy,Medium,Hard,Insane}

    private BrawlChoice PlayerMove = BrawlChoice.nullChoice;
    private int winCounter = 0;
    private bool stopGame = false;

    //Player Choice Registered through button clicks, change to keyboard input if desired
    public void onClick(string choice) {
        switch (choice) {
            case "Rock":
                PlayerMove = BrawlChoice.Rock;
                break;
            case "Paper":
                PlayerMove = BrawlChoice.Paper;
                break;
            case "Scissors":
                PlayerMove = BrawlChoice.Scissors;
                break;
            default:
                PlayerMove = BrawlChoice.nullChoice;
                break;
        }
    }
    //Scene Startup - Load Game Music
    void Start() {
        StartCoroutine(StartMusicWait());
    }

    //Start Game with intro music and then loop music based on difficulty
    private IEnumerator StartMusicWait() {
        playMusic(BrawlMusicClip.brawlStartClip); 
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        switch (difficulty) {
            case Difficulty.Easy:
                playMusic(BrawlMusicClip.brawlLoopClip120); 
                break;
            case Difficulty.Medium:
                playMusic(BrawlMusicClip.brawlLoopClip180); 
                break;
            case Difficulty.Hard:
                playMusic(BrawlMusicClip.brawlLoopClip240); 
                break;
            case Difficulty.Insane:
                playMusic(BrawlMusicClip.brawlLoopClip300); 
                break;
        }
        winCounterText.text = "Win Count: " + winCounter.ToString() + "/" + correctToWin.ToString();
        BearMove();
    }

    //Determine Bear AI Choice
    public void BearMove() {     
        BrawlChoice randomChoice = Choices[Random.Range(0, Choices.Length)]; 
        switch (randomChoice) {
            case BrawlChoice.Rock:
                Debug.Log("Rock");
                Result.text = "Rock";
                AIChoice.sprite = Rock;
                break;
            case BrawlChoice.Paper:
                Result.text = "Paper";
                Debug.Log("Paper");
                AIChoice.sprite = Paper;
                break;
            case BrawlChoice.Scissors:
                Result.text = "Scissors";
                Debug.Log("Scissors");
                AIChoice.sprite = Scissors;
                break;
        }
        StartCoroutine(PlayerWait());
    } 

    //Wait for player choice and then determine winner
    private IEnumerator PlayerWait() {
        //wait for player choice, varies depending on tempo
        float speed;
        switch (difficulty) {
            case Difficulty.Easy:
                speed = 3.0f;
                break;
            case Difficulty.Medium:
                speed = 2.25f;
                break;
            case Difficulty.Hard:
                speed = 1.5f;
                break;
            case Difficulty.Insane:
                speed = 1.1f;
                break;
            default :
                speed = 3;
                break;
        }

        //countdown timer
        timerText.text = "3";
        yield return new WaitForSeconds(speed / 3.0f);
        timerText.text = "2";
        yield return new WaitForSeconds(speed / 3.0f);
        timerText.text = "1";
        yield return new WaitForSeconds(speed / 3.0f);
        timerText.text = "0";

        //check player selection
        switch (PlayerMove) {
            case BrawlChoice.Rock:
                if (AIChoice.sprite == Scissors) {
                    Win();
                    break;
                } else if (AIChoice.sprite == Rock) {
                    Draw();
                    break;
                } else if (AIChoice.sprite == Paper) {
                    Lose();
                    break;
                }
                break; 
            case BrawlChoice.Paper:
                if (AIChoice.sprite == Rock) {
                    Win();
                    break;
                } else if (AIChoice.sprite == Paper) {
                    Draw();
                    break;
                } else if (AIChoice.sprite == Scissors) {
                    Lose();
                    break;
                }
                break;
            case BrawlChoice.Scissors:  
                if (AIChoice.sprite == Paper) {
                    Win();
                    break;
                } else if (AIChoice.sprite == Scissors) {
                    Draw();
                    break;
                } else if (AIChoice.sprite == Rock) {
                    Lose();
                    break;
                }
                break;
            default:
                Lose();
                yield break; 
        }   

        //pause briefly before next round based on tempo
        yield return new WaitForSeconds(speed / 3.0f);   
        PlayerMove = BrawlChoice.nullChoice;

        //if game is not over, repeat
        if (!stopGame) {
            switch (difficulty) {
            case Difficulty.Easy:
                playMusic(BrawlMusicClip.brawlLoopClip120); 
                break;
            case Difficulty.Medium:
                playMusic(BrawlMusicClip.brawlLoopClip180); 
                break;
            case Difficulty.Hard:
                playMusic(BrawlMusicClip.brawlLoopClip240); 
                break;
            case Difficulty.Insane:
                playMusic(BrawlMusicClip.brawlLoopClip300); 
                break;
            }
            BearMove();
        } 
    } 

    //If player won current round, also checks if won necessary rounds to win game
    private void Win() {
        Result.text = "Good!";
        Debug.Log("Good!");
        winCounter += 1;
        winCounterText.text = "Win Count: " + winCounter.ToString() + "/" + correctToWin.ToString();
        if (winCounter >= correctToWin) {
            Result.text = "You Win the Game!";
            timerText.text = "";
            winCounterText.text = "";
            Debug.Log("You Win the Game!");
            GetComponent<AudioSource>().Stop();
            stopGame = true;
            playMusic(BrawlMusicClip.brawlVictoryClip);
            //TODO could insert win screen here

            // Hayes here. I replaced the restart with selecting a random game when you win
            LevelManager.GetComponent<Transition>().WinMiniGame(4.5f);
            //StartCoroutine(RestartGame(4.5f));
        }
    }

    //If player and bear draw, no points are awarded - consider changing somehow?
    private void Draw() {
        Result.text = "Draw!";
        Debug.Log("Draw!");
    }

    //If player loses current round, game over
    private void Lose() {
        //TODO Add game over / discovered animation or screen
        Result.text = "You've been Discovered!";
        stopGame = true;
        Debug.Log("You've been Discovered!");
        StartCoroutine(Discovered());
        
    }

    //play discovered music and then restart game
    private IEnumerator Discovered() {
        playMusic(BrawlMusicClip.brawlMistakeClip); 
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        playMusic(BrawlMusicClip.brawlDiscoveredClip);
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        StartCoroutine(RestartGame(0));
    }

    //restart game after input seconds
    private IEnumerator RestartGame(float seconds) {
        Debug.Log("Game Restarting...");
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart scene
    }

    //change music based on clip number
    private void playMusic(BrawlMusicClip clipNum) {
        switch (clipNum) {
            case BrawlMusicClip.brawlStartClip:
                GetComponent<AudioSource>().clip = brawlStartClip;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlLoopClip120:
                GetComponent<AudioSource>().clip = brawlLoopClip120;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlLoopClip180:
                GetComponent<AudioSource>().clip = brawlLoopClip180;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlLoopClip240:
                GetComponent<AudioSource>().clip = brawlLoopClip240;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlLoopClip300:
                GetComponent<AudioSource>().clip = brawlLoopClip300;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlMistakeClip:
                GetComponent<AudioSource>().clip = brawlMistakeClip;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlDiscoveredClip:
                GetComponent<AudioSource>().clip = brawlDiscoveredClip;
                GetComponent<AudioSource>().Play();
                break;
            case BrawlMusicClip.brawlVictoryClip:
                GetComponent<AudioSource>().clip = brawlVictoryClip;
                GetComponent<AudioSource>().Play();
                break;
        }
    }

}


/*
To-Do:
Main Functionality Complete, need to polish when art + proper design is complete


Extra Features:
-Add Proper cutscenes for win/lose + Better Art
-Convert theme to bear fighting themed and add more options (claws, bite, etc.) 4-5 maybe?
-Be able to change the location of the AIChoice Image depending on which one selected (high, low, etc.)
-add keyboard functionality instead of clickable buttons?

-Add a "Bear Rage" mode where the bear will always win for a few rounds [github copilot's random idea]
-Add special rounds that hide the bear's choice from the player occasionally?
*/
