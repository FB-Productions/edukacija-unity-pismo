using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int life = 3;
    public TMP_Text scoreText;
    public TMP_Text lifeText;
    public string[] chainHistory;
    public TMP_Text chainText;

    private void Update()
    {
        scoreText.text = "Score: " + score;
        lifeText.text = "Life: " + life + " / 3";
        //chainText = "Chain: " + chainHistory[0];
        //Debug.Log("Chain: " + chainHistory[0] + " " + chainHistory[1] + " " + chainHistory[2]);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit");
        }

        if (life <= 0)
        {
            Debug.Log("Lose");
        }
    }
}
