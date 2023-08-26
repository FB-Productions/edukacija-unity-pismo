using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textFieldArray; // text svih polja

    [SerializeField] TextMeshProUGUI scorePlayerOne; // iznos bodova playera 1 na game panelu
    [SerializeField] TextMeshProUGUI scorePlayerTwo; // -//- playera 2 -//-
    [SerializeField] TextMeshProUGUI movesText; // ispis broja poteza

    [SerializeField] string playerSide; // moze imate vrijednost X ili O

    int moves; // brojac poteza

    private void Start()
    {
        playerSide = "X";
        moves = 1;

        for (int i = 0; i < textFieldArray.Length; i++)
        {
            textFieldArray[i].text = null;
        }

        movesText.text = "Move " + moves;

        scorePlayerOne.text = "Player One: "; // Napravi player prefs
        scorePlayerTwo.text = "Player Two: ";
    }
}
