using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField] string _nextLevelName;
    public void setText(string text)
    {
        SceneManager.LoadScene(_nextLevelName);

    }
}
