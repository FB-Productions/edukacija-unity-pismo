using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    float transitionBloomStart = 1;
    float transitionBloomEnd = 0.01f;
    [SerializeField] PostProcessVolume transitionBloomVolume;
    [SerializeField] AudioSource menuMusic;

    private void Awake()
    {
        transitionBloomVolume.weight = transitionBloomStart;
    }

    private void Start()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        while (transitionBloomVolume.weight > transitionBloomEnd)
        {
            yield return null;
            transitionBloomVolume.weight -= Time.deltaTime;
        }
        transitionBloomVolume.weight = transitionBloomEnd;
        menuMusic.Play();
    }

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
