using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int life = 100;
    int lifeMax;
    public float invincibility;
    public float score = 0;
    public float scoreMax;
    //int pickupsRemaining;
    public GameObject invEffect;
    public TMP_Text textScore;
    public TMP_Text textLife;
    public TMP_Text textWin;
    public TMP_Text textLose;
    public bool win;
    public bool lose;
    AudioSource mus;
    float unscaledTimer;
    float unscaledTimerInit = 2.5f;
    float levelTime;
    public TMP_Text textTime;

    private void Start()
    {
        lifeMax = life;
        UpdateScoreText();
        UpdateLifeText();
        mus = GetComponent<AudioSource>();
        unscaledTimer = unscaledTimerInit;
    }

    private void Update()
    {
        if (!win && !lose)
        {
            levelTime += Time.unscaledDeltaTime;
        }
        
        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
            if (invincibility < 0)
            {
                invincibility = 0;
            }
        }

        if (invincibility > 0)
        {
            invEffect.SetActive(true);
        }
        else
        {
            invEffect.SetActive(false);
        }

        if (win || lose)
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale -= Time.deltaTime * Time.timeScale * 10;
                if (Time.timeScale < 0.025f)
                {
                    Time.timeScale = 0;
                }
            }

            if (mus.volume > 0)
            {
                mus.volume -= Time.deltaTime * mus.volume * 10;
                if (mus.volume < 0.025f)
                {
                    mus.volume = 0;
                }
            }

            unscaledTimer -= Time.unscaledDeltaTime;
            if (unscaledTimer < 0)
            {
                if (win)
                {
                    Win();
                }
                else if (lose)
                {
                    Lose();
                }
                unscaledTimer = unscaledTimerInit;
            }
        }
    }

    public void LoseLife (int damage, float invincibilityTime)
    {
        if (invincibility <= 0)
        {
            life -= damage;
            if (life < 0)
                life = 0;

            UpdateLifeText();

            if (life <= 0)
            {
                if (!win && !lose)
                {
                    //Debug.LogError("LOSE GAME, your score is: " + score);
                    lose = true;
                    // TODO deactivate all objects and display a screenshot
                    // ili Time.scale = 0 i ugasi komponente koje primaju inpute (igraca i sl.)
                }
            }
        }

        if (invincibilityTime > invincibility)
        {
            invincibility = invincibilityTime;
        }
    }

    public void UpdateScoreText()
    {
        if (!win && !lose)
            textScore.text = "Score: " + score + "/" + scoreMax;
    }

    public void UpdateLifeText()
    {
        if (!win && !lose)
            textLife.text = "Life: " + life + "/" + lifeMax;
    }

    void Win()
    {
        textWin.gameObject.SetActive(true);
    }

    void Lose()
    {
        textLose.gameObject.SetActive(true);
    }

    public void ShowTime()
    {
        textTime.gameObject.SetActive(true);
        int min = (int)levelTime / 60;
        int sek = (int)levelTime % 60;
        textTime.text = "Time: " + string.Format("{0:00}:{1:00}", min, sek);
    }
}
