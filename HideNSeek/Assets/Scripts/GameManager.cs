using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public int minutesHiding = 1;
    public int minutesSeeking = 5;
    public bool isRemote;
    [Header("Refferences")]
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
    public GameObject remoteHiderEndMenu;
    public TMP_InputField seekCodeField;
    float timer;
    bool seeking; // false = hiding phase; true = seeking phase
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
            if (!seeking) // hiding over
            {
                if (canHide)
                {
                    EndHide();
                }
                else
                {
                    timer = 0;
                }
                
            }
            else // seeking over
            {
                if (!roundOver)
                {
                    //seekTimerUI.SetActive(false);
                    Winner("Hider"); // hider wins
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

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    string GenerateSeekCode()
    {
        LevelGenerator.Scripts.LevelGenerator lg = FindObjectOfType<LevelGenerator.Scripts.LevelGenerator>();
        string seekCode = lg.Seed + "\n" + hiderPM.gameObject.transform.position /*+ "\n" + hiderPM.transform.rotation*/;
        // encode base64
        return seekCode;
    }

    public void CopySeekCode()
    {
        GUIUtility.systemCopyBuffer = seekCodeField.text;
    }

    public void EndHide()
    {
        timer = minutesSeeking * 60;
        seeking = true;
        if (!isRemote)
        {
            hiderCam.SetActive(false);
        }
        hiderPM.enabled = false;
        if (!isRemote)
        {
            seeker.SetActive(true);
        }
        hideTimerUI.SetActive(false);
        if (!isRemote)
        {
            seekTimerUI.SetActive(true);
        }

        if (isRemote) // when playing a remote game, here we show the seek code and go back to the menu
        {
            Time.timeScale = 0;
            seekCodeField.text = GenerateSeekCode();
            remoteHiderEndMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
