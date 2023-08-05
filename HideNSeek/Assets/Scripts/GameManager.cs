using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Postavke")]
    public int minutesHiding = 1;
    public int minutesSeeking = 5;
    [Header("Reference")]
    public GameObject hiderCam;
    public PlayerMovement hiderPM;
    public GameObject seeker;
    public GameObject hideTimerUI;
    public GameObject seekTimerUI;
    public GameObject winnerUI;
    TextMeshProUGUI hideTimerText;
    TextMeshProUGUI seekTimerText;
    TextMeshProUGUI winnerText;
    public GameObject retryButton;
    float timer;
    bool seeking; // false = faza skrivanja; true = faza trazenja
    public bool canHide;
    bool roundOver;


    private void Start()
    {
        timer = minutesHiding * 60;
        hideTimerText = hideTimerUI.GetComponent<TextMeshProUGUI>();
        seekTimerText = seekTimerUI.GetComponent<TextMeshProUGUI>();
        winnerText = winnerUI.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (!roundOver)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                timer = 0;
            }
        }

        if (timer <= 0)
        {
            if (!seeking) // kraj skrivanja
            {
                if (canHide)
                {
                    timer = minutesSeeking * 60;
                    seeking = true;
                    hiderCam.SetActive(false);
                    hiderPM.enabled = false;
                    seeker.SetActive(true);
                    hideTimerUI.SetActive(false);
                    seekTimerUI.SetActive(true);
                }
                else
                {
                    timer = 0;
                }
                
            }
            else // kraj trazenja
            {
                if (!roundOver)
                {
                    //seekTimerUI.SetActive(false);
                    Winner("Hider"); // hider pobjedjuje
                }
            }
        }
        else
        {
            if (!seeking)
            {
                int tSek = (int)timer % 60;
                int tMin = (int)timer / 60;

                hideTimerText.text = "Time to Hide " + string.Format("{0:00}:{1:00}", tMin, tSek);
            }
            else
            {
                int tSek = (int)timer % 60;
                int tMin = (int)timer / 60;

                seekTimerText.text = "Time to Seek " + string.Format("{0:00}:{1:00}", tMin, tSek);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Winner(string winnerName)
    {
        seekTimerUI.SetActive(false);
        roundOver = true;
        Time.timeScale = 0;
        winnerUI.SetActive(true);
        winnerText.text = winnerName + " Wins";
        retryButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SceneRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
