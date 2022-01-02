using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{

    private int number;
    private float timer;
    private float timer2;

    public Text text;
    public Enemy enemy;
    public Bandit player;

   
    void Update()
    {
        timer += Time.deltaTime;
        number = (int)timer;

       text.text = "" + number;

        if (enemy.enabled == false)
        {
            text.text = "Ukończono wstęp!";
            
         timer2 += Time.deltaTime;
            if (timer2 > 2)
                SceneManager.LoadScene("Wybór misji");
        }
        //if (player.enabled == false)
       // {//
            //text.text = "Przegrana!";
          
           // timer2 += Time.deltaTime;
           // if (timer2 > 2)
             //  Application.Quit();
        //}

    }
}
