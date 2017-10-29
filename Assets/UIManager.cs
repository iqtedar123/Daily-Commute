using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text scoreText;
    public Text pausedText;
    public Text timeText;
    public Text level1TimeText;
    public Text level1ScoreText;
    public Text alertText;
    public Image level2Image;
    public Sprite whiteBackgroundSprite;
    public Sprite level2BackgreoundSprite;
    public Button goTrainButton;
    public Button pokemonGoButton;
    public Button finishedLevel2Button;
    public AudioClip gameWon;
    public AudioClip gameLost; 
    public AudioClip defaultSound;
    public AudioSource level2Source;
    public Text finalScoreText;
    float timer = 0.0f;

    float timerMax = 3.0f;
    float level1Time = 60.0f;
    float level2Time = 10.0f;

    public static int score;
    static int level1Score;
    bool playerDestroyed;
    bool isPaused = false;
    bool activateNextLevel = false;
    public bool level2 = false;
    bool wonLevel2 = false;
    public static bool finishedLevel2Bool = false;
    // Use this for initialization
    void Start () {
        score = 0;
        if (pausedText != null)
        {
            pausedText.enabled = false;
        }
        InvokeRepeating("scoreUpdate", 1.0f, 1.0f);
        if(alertText != null)
        {
            alertText.enabled = false;
        }
	}
    void Awake()
    {
    }
    // Update is called once per frame
    void Update() {
        if(finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + level1Score;
        }
        if(finishedLevel2Bool == false)
        {
            if (finishedLevel2Button != null)
            {
                finishedLevel2Button.interactable = false;
            }
            if (pokemonGoButton != null)
            {
                pokemonGoButton.interactable = true;
            }
            if (goTrainButton != null)
            {
                goTrainButton.interactable = true;
            }
        }
        if(level2 == true)
        {
            if(timer >= level2Time)
            {
                //User did not choose in time.
                //Disable current image. 
                level2Image.sprite = whiteBackgroundSprite;
                //Display timeout message.
                alertText.enabled = true;
                alertText.text = "Timeout! Press to continue";
                alertText.fontSize = 100;
                level2 = false;
                if(pokemonGoButton != null)
                {
                    pokemonGoButton.interactable = false;
                }
                if(goTrainButton != null)
                {
                    goTrainButton.interactable = false;
                }
                if (finishedLevel2Button != null)
                {
                    finishedLevel2Button.interactable = true;
                }
                timeText.text = "";
                wonLevel2 = false;
                finishedLevel2Bool = true;
                if (level2Source != null)
                {
                    level2Source.clip = gameLost;
                    level2Source.Play();
                }
            }
            else
            {
                float currentTime = 10.0f - timer;
                if (timeText != null) {
                    timeText.text = "" + currentTime.ToString("F0");
                }
                if (finishedLevel2Button != null)
                {
                    finishedLevel2Button.interactable = false;
                }
                if (pokemonGoButton != null)
                {
                    pokemonGoButton.interactable = true;
                }
                if (goTrainButton != null)
                {
                    goTrainButton.interactable = true;
                }
            }
        }


        if (level1Time != 0.0f) { 
        if (timer >= level1Time)
        {
            //Reached high score. proceed to next level
            level1Score = score;
            SceneManager.LoadScene("CompletedDrive");
            level1Time = 0.0f;
        }
        else
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }

        }
    }
        if(level1ScoreText != null)
        {
            level1ScoreText.text = "Score: " + level1Score;
        }
        timer += Time.deltaTime;
        if (level1TimeText != null)
        {
            float remainingTimeLevel1 = level1Time - timer;
            level1TimeText.text = "" + remainingTimeLevel1.ToString("F0");
        }
        if (timer >= timerMax)
        {
            activateNextLevel = true;


        }
    }
    void scoreUpdate()
    {
        int multiplier = 5;
        if (!playerDestroyed)
        {
            if(timer > 10 && timer <= 30)
            {
                multiplier = 15;
            }else if(timer > 30 && timer<= 60)
            {
                multiplier = 20;
            }
            //score += multiplier;
        }
    }
    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            isPaused = true;
            if (pausedText != null)
            {
                pausedText.enabled = true;
            }
        }
        else if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            isPaused = false;
            if (pausedText != null)
            {
                pausedText.enabled = false;
            }
        }
    }
    public void Play()
    {

        SceneManager.LoadScene("Introduction");
    }
    public void gameOver()
    {
        isPaused = false;
        if (pausedText != null)
        {
            pausedText.enabled = false;
        }
        level1Score = score;
        playerDestroyed = true;
    }
    public void Retry()
    {
        Debug.Log("Finsished lvel 2 retry: " + finishedLevel2Bool);
        if (finishedLevel2Bool == false)
        {
            finishedLevel2Bool = false;
            SceneManager.LoadScene("Level1");
        }
        else
        {
            finishedLevel2Bool = false;
            SceneManager.LoadScene("Choose_Level_2");
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Skip()
    {
        PlayGame();
    }
    public void PlayGame()
    {
        isPaused = false;
        if (pausedText != null)
        {
            pausedText.enabled = false;
        }
        SceneManager.LoadScene("Level1");
    }
    public void goToLevel2()
    {
        //Loads level 2.
        if (activateNextLevel)
        {
            //Now skip.
            level2 = true;
            SceneManager.LoadScene("Choose_Level_2");
        }
    }
    public void openedPokemon()
    {
        //Skip to game over screen!
        level2 = false;
        level2Image.sprite = whiteBackgroundSprite;
        alertText.enabled = true;
        alertText.text = "You chose wrong! You missed the train!\n" +
            "Press to continue.";
        alertText.fontSize = 100;
        if (pokemonGoButton != null)
        {
            pokemonGoButton.interactable = false;
        }
        if (goTrainButton != null)
        {
            goTrainButton.interactable = false;
        }
        if (finishedLevel2Button != null)
        {
            finishedLevel2Button.interactable = true;
        }
        timeText.text = "";
        wonLevel2 = false;
        finishedLevel2Bool = true;
        if (level2Source != null)
        {
            level2Source.clip = gameLost;
            level2Source.Play();
        }
    }
    public void enterToronto()
    {
        //player choose go train! They passed the level.
        level2 = false;
        level2Image.sprite = whiteBackgroundSprite;
        alertText.enabled = true;
        alertText.text = "Correct Choice! You made it to work on time! \n" +
            "Press to continue.";
        alertText.fontSize = 100;
        if (pokemonGoButton != null)
        {
            pokemonGoButton.interactable = false;
        }
        if (goTrainButton != null)
        {
            goTrainButton.interactable = false;
        }
        if (finishedLevel2Button != null)
        {
            finishedLevel2Button.interactable = true;
        }
        timeText.text = "";
        wonLevel2 = true;
        finishedLevel2Bool = true;
        if(level2Source != null)
        {
            level2Source.clip = gameWon;
            level2Source.Play();
        }
    }
    public void finishedLevel2()
    {
        Debug.Log("finished level 2: " + finishedLevel2Bool);
        if (level2Source != null)
        {
            level2Source.clip = defaultSound;
            level2Source.Play();
        }
        if (wonLevel2 == true) {
            finishedLevel2Bool = false;
            SceneManager.LoadScene("YouWin");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void openMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void openHelp()
    {
        SceneManager.LoadScene("Help");
    }
    public void goBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void openOtherApps()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=TechMeister786");
    }
}
