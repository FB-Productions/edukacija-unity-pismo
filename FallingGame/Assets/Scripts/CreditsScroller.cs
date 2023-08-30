using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroller : MonoBehaviour
{
    public Scrollbar scroll;
    byte scrollPhase = 0;

    private void OnEnable()
    {
        scroll.value = 1f;
        scrollPhase = 0;
    }

    private void Start()
    {
        StartCoroutine(StartScrolling());
    }

    private void Update()
    {
        if (scrollPhase == 1)
        {
            scroll.value -= 0.1f * Time.deltaTime;
            if (scroll.value <= 0.01f)
            {
                scrollPhase = 2;
            }
        }
    }

    IEnumerator StartScrolling()
    {
        yield return new WaitForSeconds(6);
        scrollPhase = 1;
    }

    public void AutoscrollEnd()
    {
        StopAllCoroutines();
        scrollPhase = 2;
    }
}
