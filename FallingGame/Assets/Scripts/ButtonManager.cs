using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void NotImplemented()
    {
        Debug.Log("Not implemented");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
