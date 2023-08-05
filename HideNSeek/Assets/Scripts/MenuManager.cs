using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle levelToggle;
    public TMP_InputField seedInputField;
    public TMP_InputField seekCodeInputField;
    bool randomLevel = true;
    int seed;
    bool isRemote;
    bool hasSeekCode;
    Vector3 SC_hiderPos;
    //Vector3 SC_hiderRot;

    private void Awake()
    {
        Debug.Log("Awake Seed: " + seed);
        Debug.Log("Awake RandomLevel bool: " + randomLevel);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "HideNSeek")
        {
            //Destroy(gameObject);
        }
    }

    IEnumerator DoYourThing()
    {
        yield return new WaitForFixedUpdate();
        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log("Awake Seed: " + seed);
        Debug.Log("Awake RandomLevel bool: " + randomLevel);

        if (SceneManager.GetActiveScene().name == "HideNSeek")
        {
            GameManager gm = FindObjectOfType<GameManager>();
            LevelGenerator.Scripts.LevelGenerator lg = FindObjectOfType<LevelGenerator.Scripts.LevelGenerator>();

            if (!randomLevel)
            {
                lg.Seed = seed;
                Debug.Log("LG got: " + seed);
            }

            gm.isRemote = isRemote;

            if (hasSeekCode)
            {
                gm.hiderPM.gameObject.transform.position = SC_hiderPos;
                //gm.hiderPM.gameObject.transform.rotation = SC_hiderRot;
                gm.EndHide();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HideNSeek");
        StartCoroutine(DoYourThing());
    }

    public void HostGame()
    {
        isRemote = true;
        StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleLevel()
    {
        if (levelToggle.isOn)
        {
            seedInputField.gameObject.SetActive(false);
            randomLevel = true;
        }
        else
        {
            seedInputField.gameObject.SetActive(true);
            randomLevel = false;
        }
    }

    public void UpdateSeed()
    {
        seed = int.Parse(seedInputField.text);
        Debug.Log("Updated seed:" + seed);
    }

    public void ParseSeekCode()
    {
        // decode base64
        string[] seekCode = seekCodeInputField.text.Split("\n");
        seed = int.Parse(seekCode[0]);
        randomLevel = false;
        string[] SC_hiderPos_str = seekCode[1].Split(",");
        SC_hiderPos = new Vector3(float.Parse(SC_hiderPos_str[0]), float.Parse(SC_hiderPos_str[1]), float.Parse(SC_hiderPos_str[2]));
        StartGame();
    }
}
