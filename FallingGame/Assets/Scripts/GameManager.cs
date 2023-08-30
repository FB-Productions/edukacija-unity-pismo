using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Values")]
    public int score = 0;
    public int life = 3;
    public string[] chainHistory;
    bool gameOver;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text lifeText;
    public TMP_Text chainText;

    [Header("UI Groups")]
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;

    [Header("Other")]
    [SerializeField] PostProcessVolume vignette;
    [SerializeField] AudioSource music;
    float vignetteFadeInValue = 1;
    float vignetteFadeOutValue = 0;

    private void Start()
    {
        gameOver = false;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        lifeText.text = "Life: " + life + " / 3";
        //chainText = "Chain: " + chainHistory[0];
        //Debug.Log("Chain: " + chainHistory[0] + " " + chainHistory[1] + " " + chainHistory[2]);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HUD.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            music.volume = 0.3f;
            StartCoroutine(VignetteFadeIn());
            /*Application.Quit();
            Debug.Log("Quit");*/
        }

        if (!gameOver)
        {
            if (life <= 0)
            {
                HUD.SetActive(false);
                gameOverMenu.SetActive(true);
                Time.timeScale = 0;
                music.Stop();
                StartCoroutine(VignetteFadeIn());
                gameOver = true;
                Debug.Log("Lose");
            }
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator VignetteFadeIn()
    {
        while (vignette.weight < vignetteFadeInValue)
        {
            yield return null;
            vignette.weight += Time.deltaTime;
        }
        vignette.weight = vignetteFadeInValue;
    }
    public IEnumerator VignetteFadeOut()
    {
        while (vignette.weight > vignetteFadeOutValue)
        {
            yield return null;
            vignette.weight -= Time.deltaTime;
        }
        vignette.weight = vignetteFadeOutValue;
    }

}
