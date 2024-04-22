using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BearRockPaperScissors : MonoBehaviour
{
    public Image AIChoice; 
    public GameObject bearOpponent;
    public GameObject arms;
    public BrawlChoice[] Choices;
    public Sprite Rock, Paper, Scissors;
    public Sprite armsBlockBite, armsBlockLeft, armsBlockRight, armDefault;
    public Sprite bearBite, bearLeft, bearRight, bearOpponentDefault;
    public Sprite bearBite2, bearLeft2, bearRight2;
    public TextMeshProUGUI Result;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winCounterText;
    public Difficulty initialDifficulty = Difficulty.Easy;
    private Difficulty difficulty;
    public int correctToWin = 3;
    public GameObject LevelManager;
    private Sprite armSprite;
    private Sprite bearSprite;
    

    public AudioClip brawlStartClip;
    public AudioClip  brawlLoopClip120;
    public AudioClip  brawlLoopClip180;
    public AudioClip  brawlLoopClip240;
    public AudioClip  brawlLoopClip300;
    public AudioClip  brawlMistakeClip;
    public AudioClip  brawlDiscoveredClip;
    public AudioClip brawlVictoryClip;
    public AudioClip armSwingClip;
    public AudioClip punchClip;

    public enum BrawlChoice{Rock,Paper,Scissors,nullChoice}
    public enum BrawlMusicClip {brawlStartClip,brawlLoopClip120,brawlLoopClip180,brawlLoopClip240,brawlLoopClip300,
                                brawlMistakeClip,brawlDiscoveredClip,brawlVictoryClip}
    public enum Difficulty{Easy,Medium,Hard,Insane}

    private BrawlChoice PlayerMove = BrawlChoice.nullChoice;
    private int winCounter = 0;
    private bool stopGame = false;
    //private int timer = 3;

    //Player Choice Registered through button clicks, change to keyboard input if desired
    // public void onClick(string choice) {
    //     switch (choice) {
    //         case "Rock":
    //             PlayerMove = BrawlChoice.Rock;
    //             break;
    //         case "Paper":
    //             PlayerMove = BrawlChoice.Paper;
    //             break;
    //         case "Scissors":
    //             PlayerMove = BrawlChoice.Scissors;
    //             break;
    //         default:
    //             PlayerMove = BrawlChoice.nullChoice;
    //             break;
    //     }
    // }


    
    public void Update() {
        //get keyboard input for rock paper scissors (left, down, right) / (a, s, d)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            PlayerMove = BrawlChoice.Rock;
            Debug.Log("Player: Rock, BlockLeft");
            //change player arm sprite to rock
            arms.GetComponent<SpriteRenderer>().sprite = armsBlockLeft;
            GetComponent<AudioSource>().PlayOneShot(armSwingClip);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            PlayerMove = BrawlChoice.Paper;
            Debug.Log("Player: Paper, BlockBite");
            arms.GetComponent<SpriteRenderer>().sprite = armsBlockBite;
            GetComponent<AudioSource>().PlayOneShot(armSwingClip);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            PlayerMove = BrawlChoice.Scissors;
            arms.GetComponent<SpriteRenderer>().sprite = armsBlockRight;
            Debug.Log("Player: Scissors, BlockRight");
            GetComponent<AudioSource>().PlayOneShot(armSwingClip);
        }
    }

    //Scene Startup - Load Game Music
    void Start() {
        difficulty = initialDifficulty;
        StartCoroutine(StartMusicWait());
        StartCoroutine(StartBear());
        //armSprite = arms.GetComponent<SpriteRenderer>().sprite;
        //bearSprite = bearOpponent.GetComponent<SpriteRenderer>().sprite;
    }

    private IEnumerator StartBear() {
        yield return new WaitForSeconds(2.6f);
        bearOpponent.GetComponent<SpriteRenderer>().sprite = bearBite2;
        yield return new WaitForSeconds(0.5f);
        //bearOpponent.transform.position -= new Vector3(0.2f, 0.4f, 0);
       
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
        arms.GetComponent<SpriteRenderer>().sprite = armDefault;   
        BrawlChoice randomChoice = Choices[Random.Range(0, Choices.Length)]; 
        switch (randomChoice) {
            case BrawlChoice.Rock:
                Debug.Log("Bear: Rock, BearBite");
                Result.text = "Rock, BearBite";
                AIChoice.sprite = Rock;
                bearOpponent.GetComponent<SpriteRenderer>().sprite = bearBite;
                break;
            case BrawlChoice.Paper:
                Result.text = "Paper, BearRight";
                Debug.Log("Bear: Paper, BearRight");
                AIChoice.sprite = Paper;
                bearOpponent.GetComponent<SpriteRenderer>().sprite = bearRight;
                break;
            case BrawlChoice.Scissors:
                Result.text = "Scissors, BearLeft";
                Debug.Log("Bear: Scissors, BearLeft");
                AIChoice.sprite = Scissors;
                bearOpponent.GetComponent<SpriteRenderer>().sprite = bearLeft;
                break;
        }
        StartCoroutine(PlayerWait(randomChoice));
    } 

    //Wait for player choice and then determine winner
    private IEnumerator PlayerWait(BrawlChoice randomChoice) {
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
        //timer = 3;
        timerText.text = "3";
        yield return new WaitForSeconds(speed / 3.0f);
        //timer = 2;
        timerText.text = "2";
        yield return new WaitForSeconds(speed / 3.0f);
        //UPDATE BEARSPRITE
        switch (randomChoice) {
            case BrawlChoice.Rock:
                bearOpponent.GetComponent<SpriteRenderer>().sprite = bearBite2;
                bearOpponent.transform.position -= new Vector3(0.2f, 0.4f, 0);
                break;
            case BrawlChoice.Paper:
                bearOpponent.GetComponent<SpriteRenderer>().sprite = bearRight2;
                bearOpponent.transform.position -= new Vector3(0.2f, 0.4f, 0);
                break;
            case BrawlChoice.Scissors:
                bearOpponent.GetComponent<SpriteRenderer>().sprite = bearLeft2;
                bearOpponent.transform.position -= new Vector3(0.2f, 0.4f, 0);
                break;
        }


        //timer = 1;
        timerText.text = "1";
        yield return new WaitForSeconds(speed / 3.0f);
        
        //timer = 0;
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
            bearOpponent.transform.position = new Vector3(0.69f, -2.43f, 0);
            //timer = 3;

            //set difficulty to gradually increase speed of game
            if (initialDifficulty == Difficulty.Easy) {
                if (winCounter < 3) { difficulty = Difficulty.Easy; }
                else if (winCounter < 6) { difficulty = Difficulty.Medium; }
                else if (winCounter < 9) { difficulty = Difficulty.Hard; }
                else if (winCounter < 12) { difficulty = Difficulty.Insane; }
            }
            else if (initialDifficulty == Difficulty.Medium) {
                if (winCounter < 3) { difficulty = Difficulty.Medium; }
                else if (winCounter < 6) { difficulty = Difficulty.Hard; }
                else if (winCounter < 9) { difficulty = Difficulty.Insane; }
                else if (winCounter < 12) { difficulty = Difficulty.Insane; }
            }
            else if (initialDifficulty == Difficulty.Hard) {
                if (winCounter < 3) { difficulty = Difficulty.Hard; }
                else if (winCounter < 6) { difficulty = Difficulty.Hard; }
                else if (winCounter < 9) { difficulty = Difficulty.Insane; }
                else if (winCounter < 12) { difficulty = Difficulty.Insane; }
            } else if (initialDifficulty == Difficulty.Insane) {
                if (winCounter < 3) { difficulty = Difficulty.Insane; }
                else if (winCounter < 6) { difficulty = Difficulty.Insane; }
                else if (winCounter < 9) { difficulty = Difficulty.Insane; }
                else if (winCounter < 12) { difficulty = Difficulty.Insane; }
            }



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
            bearOpponent.GetComponent<SpriteRenderer>().sprite = bearOpponentDefault;
            //bearOpponent.transform.position -= new Vector3(0, 0.5f, 0);
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
        GetComponent<AudioSource>().PlayOneShot(punchClip);
        Result.text = "You've been Discovered!";
        stopGame = true;
        Debug.Log("You've been Discovered!");
        StartCoroutine(Discovered());
        LevelManager.GetComponent<Transition>().LoseMiniGame(0.8f);
    }

    //play discovered music and then restart game
    private IEnumerator Discovered() {
        playMusic(BrawlMusicClip.brawlMistakeClip); 
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        //playMusic(BrawlMusicClip.brawlDiscoveredClip);
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
