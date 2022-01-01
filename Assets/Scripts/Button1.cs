using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button1 : MonoBehaviour
{

    public Button button;
    void Start()
    {
        button.onClick.AddListener(Next);
    }

    private void Next()
    {
        SceneManager.LoadScene("Start");
    }
}
