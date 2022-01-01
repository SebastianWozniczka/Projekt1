using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Button button,button2;
    public Text text;
    public AudioSource source;

    private string tekst;
    void Start()
    {
        button2.onClick.AddListener(TriggerDialogue);
        tekst = text.text;
        text.text = "";

        StartCoroutine(TypeSentence(tekst));
        source.Play();
    }


    IEnumerator TypeSentence(string sentence)
    {
       
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void TriggerDialogue()
    {
        source.Stop();
        StopAllCoroutines();
        text.text = tekst;
   
    }

}
