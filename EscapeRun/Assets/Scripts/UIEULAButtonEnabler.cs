using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIEULAButtonEnabler : MonoBehaviour
{
    public Button agree;
    Scrollbar scroll;

    private void Start()
    {
        scroll = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        if (scroll.value <= 0.05)
        {
            agree.interactable = true;
        }
    }
}
