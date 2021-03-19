using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    public int points = 0;

    AudioSource audioSource;

    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public MusicScript musicScript;

    bool lessTime = false;

    PostProcessProfile normalProfile;
    PostProcessProfile lessTimeProfile;
    PostProcessVolume volume;

    public Text timeText;
    public Text goldKeyText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text crystalText;
    public Image snowFlake;

    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true; //<-----
        InvokeRepeating("Stopper", freez, 1);
    }

    public void AddPoints(int point)
    {
        points += point;
        crystalText.text = points.ToString(); //<-----
    }

    public void AddTime(int addTime) 
    {
        timeToEnd += addTime;
        timeText.text = timeToEnd.ToString(); //<-----
    }

    public void AddKey(KeyColor color)
    {
        switch (color)
        {
            case KeyColor.Red:
                redKey++;
                redKeyText.text = redKey.ToString(); //<-----
                break;
            case KeyColor.Green:
                greenKey++;
                greenKeyText.text = greenKey.ToString(); //<-----
                break;
            case KeyColor.Gold:
                goldKey++;
                goldKeyText.text = goldKey.ToString(); //<-----
                break;
        }
    }

    void Start()
    {
       if(gameManager == null)
       {
            gameManager = this;
       }

        Time.timeScale = 1f; //<-----
        if (timeToEnd <= 0)
        {
            timeToEnd = 100;
        }

        snowFlake.enabled = false; //<-----
        timeText.text = timeToEnd.ToString(); //<-----
        infoPanel.SetActive(false); //<-----
        pauseEnd.text = "Pause"; //<-----
        reloadInfo.text = ""; //<-----
        SetUseInfo(""); //<-----
        LessTimeOff();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if(endGame)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                Application.Quit();
            }
        }
    }

    void Stopper()
    {
        timeToEnd--;
        timeText.text = timeToEnd.ToString(); //<-----
        snowFlake.enabled = false; //<-----
        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }

        if (endGame)
        {
            EndGame();
        }

        if(timeToEnd < 20 && !lessTime)
        {
            LessTimeOn();
            lessTime = true;
        }

        if(timeToEnd > 20 && lessTime)
        {
            LessTimeOff();
            lessTime = false;
        }
    }

    public void PauseGame()
    {
        if (!endGame)
        {
            PlayClip(pauseClip);
            infoPanel.SetActive(true); //<-----
            musicScript.OnPauseGame();
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if (!endGame)
        {
            PlayClip(resumeClip);
            infoPanel.SetActive(false); //<-----
            musicScript.OnResumeGame();
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }

    public void EndGame()
    {
        CancelInvoke("Stopper");
        infoPanel.SetActive(true); //<-----
        Time.timeScale = 0f;
        gamePaused = true;
        if (win)
        {
            PlayClip(winClip);
            pauseEnd.text = "You Win!!!"; //<-----
            reloadInfo.text = "Reload? Y/N"; //<-----
        } else
        {
            PlayClip(loseClip);
            pauseEnd.text = "You Lose!!!"; //<-----
            reloadInfo.text = "Reload? Y/N"; //<-----
        }
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    public void LessTimeOn()
    {
        musicScript.PitchThis(1.58f);
        //volume.profile = lessTimeProfile;
    }

    public void LessTimeOff()
    {
        musicScript.PitchThis(1f);
        //volume.profile = normalProfile;
    }

    public void SetUseInfo(string info)
    {
        useInfo.text = info;
    }

    public void WinGame()
    { 
        win = true;
        endGame = true;
    }

}
