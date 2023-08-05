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
    bool randomLevel;
    int seed;

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
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HideNSeek");
        StartCoroutine(DoYourThing());
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
}
