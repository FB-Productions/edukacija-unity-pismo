using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    float count;
    // Update is called once per frame
    void Update()
    {
        count = 1f / Time.unscaledDeltaTime;
        float fps = Mathf.Round(count);
        text.text = "FPS: " + fps;
    }
}
