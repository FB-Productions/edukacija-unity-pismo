using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyButtonManager : MonoBehaviour
{
    [SerializeField] MyGameManager gm;
    Button button;
    TextMeshProUGUI text;

    private void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Mark()
    {
        if (gm.isXTurn)
        {
            text.text = "X";
        }
        else
        {
            text.text = "O";
        }

        gm.moves++;

        button.interactable = false;

        gm.isXTurn = !gm.isXTurn;

        gm.CheckForWinner();
    }
}
