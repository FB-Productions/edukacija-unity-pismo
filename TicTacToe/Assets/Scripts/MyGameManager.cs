using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] TextMeshProUGUI[] buttonsText;
    [SerializeField] GameObject[] lines;
    int lineNum;
    [HideInInspector] public bool isXTurn = true;
    [SerializeField] TextMeshProUGUI winnerText;
    [HideInInspector] public int moves;
    bool gameOver = false;
    int scoreX;
    int scoreO;
    [SerializeField] TextMeshProUGUI scoreXText;
    [SerializeField] TextMeshProUGUI scoreOText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_InputField playerOneInputField;
    [SerializeField] TMP_InputField playerTwoInputField;
    string playerOneName;
    string playerTwoName;
    bool hasPlayedBefore;

    private void Start()
    {
        /*for (int i = 0; i < buttons.Length; i++)
        {
            buttonsText[i] = buttons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }*/

        isXTurn = true;
        gameOver = false;

        //scoreXText.text = scoreX.ToString();
        //scoreOText.text = scoreO.ToString();

        scoreX = PlayerPrefs.GetInt("ScoreX", 0);
        scoreO = PlayerPrefs.GetInt("ScoreO", 0);

        scoreXText.text = scoreX.ToString();
        scoreOText.text = scoreO.ToString();

        UpdateScoreFormatting();

        if (PlayerPrefs.GetInt("hasPlayedBefore", 0) != 0)
        {
            hasPlayedBefore = true;
        }
        else
        {
            hasPlayedBefore = false;
        }
    }

    public void CheckForWinner()
    {
        // mogucnosti su ---|||/\

        //  0   1   2
        //  3   4   5
        //  6   7   8

        string str = "X";

        for (int i = 0; i <= 1; i++)
        {
            if (buttonsText[0].text == str && buttonsText[1].text == str && buttonsText[2].text == str) // prvi red
            {
                lineNum = 0;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[3].text == str && buttonsText[4].text == str && buttonsText[5].text == str) // drugi red
            {
                lineNum = 1;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[6].text == str && buttonsText[7].text == str && buttonsText[8].text == str) // treci red
            {
                lineNum = 2;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[0].text == str && buttonsText[3].text == str && buttonsText[6].text == str) // prvi stupac
            {
                lineNum = 3;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[1].text == str && buttonsText[4].text == str && buttonsText[7].text == str) // drugi stupac
            {
                lineNum = 4;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[2].text == str && buttonsText[5].text == str && buttonsText[8].text == str) // treci stupac
            {
                lineNum = 5;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[0].text == str && buttonsText[4].text == str && buttonsText[8].text == str) // gornja lijeva dijagonala
            {
                lineNum = 6;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }
            else if (buttonsText[2].text == str && buttonsText[4].text == str && buttonsText[6].text == str) // druga dijagonala
            {
                lineNum = 7;
                if (str == "X")
                {
                    AnnounceWinner(0);
                }
                else
                {
                    AnnounceWinner(1);
                }
            }

            str = "O";
        }

        if (moves >= 9 && !gameOver)
        {
            lineNum = -1;
            AnnounceWinner(2); // izjednacenje
        }
    }

    void AnnounceWinner(byte whoWon)
    {
        gameOver = true;
        
        gameOverPanel.SetActive(true);

        if (lineNum != -1)
        {
            lines[lineNum].SetActive(true);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        if (whoWon == 0)
        {
            winnerText.text = "X wins!";
            scoreX++;
            scoreXText.text = scoreX.ToString();
            PlayerPrefs.SetInt("ScoreX", PlayerPrefs.GetInt("ScoreX") + 1);
        }
        else if (whoWon == 1)
        {
            winnerText.text = "O wins!";
            scoreO++;
            scoreOText.text = scoreO.ToString();
            PlayerPrefs.SetInt("ScoreO", PlayerPrefs.GetInt("ScoreO") + 1);
        }
        else if (whoWon == 2)
        {
            winnerText.text = "It's tied!";
        }

        UpdateScoreFormatting();
    }

    public void ResetBoard()
    {
        gameOverPanel.SetActive(false);
        moves = 0;
        gameOver = false;
        isXTurn = true;
        if (lineNum != -1)
        {
            lines[lineNum].SetActive(false);
        }

        for (int i = 0; i < buttonsText.Length; i++)
        {
            buttonsText[i].text = "";
            buttons[i].interactable = true;
        }
    }

    void UpdateScoreFormatting()
    {
        if (scoreO < 10)
        {
            scoreOText.fontSize = 108;
        }
        else if (scoreO < 100)
        {
            scoreOText.fontSize = 96;
        }
        else if (scoreO < 1000)
        {
            scoreOText.fontSize = 72;
        }
        else if (scoreO < 10000)
        {
            scoreOText.fontSize = 64;
        }
        else if (scoreO < 100000)
        {
            scoreOText.fontSize = 48;
        }
        else if (scoreO < 1000000)
        {
            scoreOText.fontSize = 32;
        }
        if (scoreX < 10)
        {
            scoreXText.fontSize = 108;
        }
        else if (scoreX < 100)
        {
            scoreXText.fontSize = 96;
        }
        else if (scoreX < 1000)
        {
            scoreXText.fontSize = 72;
        }
        else if (scoreX < 10000)
        {
            scoreXText.fontSize = 64;
        }
        else if (scoreX < 100000)
        {
            scoreXText.fontSize = 48;
        }
        else if (scoreX < 1000000)
        {
            scoreXText.fontSize = 32;
        }
    }

    public void SetUpName()
    {
        if (playerOneInputField.text != "" && playerTwoInputField.text != "")
        {
            PlayerPrefs.SetString("PlayerOneName", playerOneInputField.text);
            PlayerPrefs.SetString("PlayerTwoName", playerTwoInputField.text);
            playerOneName = PlayerPrefs.GetString("PlayerOneName");
            playerTwoName = PlayerPrefs.GetString("PlayerTwoName");

            if (hasPlayedBefore == false)
            {
                hasPlayedBefore = true;
                PlayerPrefs.SetInt("hasPlayedBefore", Random.Range(1, int.MaxValue));
            }
        }
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();

        scoreX = PlayerPrefs.GetInt("ScoreX", 0);
        scoreO = PlayerPrefs.GetInt("ScoreO", 0);

        scoreXText.text = scoreX.ToString();
        scoreOText.text = scoreO.ToString();

        UpdateScoreFormatting();

        hasPlayedBefore = false;
    }
}
