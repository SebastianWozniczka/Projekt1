using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]

public class Move : MonoBehaviour
{

    Rigidbody rb;
    Vector3 startPosition;
    new Transform transform;
    public AudioSource source1;
    public Text text, text2;

    public float launchForce = 1f;
    public float _maxDragDistance = 5; 
    float timer;
    float CameraZDistance;
    bool time = false;
    int punkty = 0;
    private string kropka = ".";
   


    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        text.text = "Losowanie...";
    }

    void OnCollisionStay(Collision collision)
    {
        if (!time)
            rb.velocity = new Vector3(0, 15, 0);
        source1.Play();
    }



    void OnMouseUp()
    {
        float currentPositionX = rb.position.x;
        float currentPositionY = rb.position.y;
        float currentPositionZ = rb.position.z;
        float directionX = startPosition.x - currentPositionX;
        float directionZ = startPosition.z - currentPositionZ;

        if (!time)
            rb.AddForce(new Vector3(directionX * launchForce, 0, directionZ * launchForce));

    }


    void Update()
    {


        

        int x = (int)transform.eulerAngles.x;
        int z = (int)transform.eulerAngles.z;
   
        timer += Time.deltaTime;
        if (timer > 5)
        {
            text.fontSize = 20;
            source1.Play();
            time = true;

          

            if (x == 90 && z == 0)
            {
                text.text = "Wylosowano świnkę";
                punkty++;
                if (punkty <= 200)
                {
                    punkty++;
                }
                else
                    punkty = 200;

            }

            if (x == 0 && z == 0)
            {
                punkty++;
                if (punkty <= 150)
                {
                    punkty++;
                }
                else
                    punkty = 150;
                text.text = "Wylosowano drzwi";
            }

            if (x == 0 && z <= 180 && z > 90)
            {
                punkty = 0;
                text.text = "Wylosowano chłopaka";


            }

            if (x == 0 && z <= 270 && z > 180)
            {
                punkty = 0;
                text.text = "Wylosowano pień";


            }

            if (x == 0 && z < 90 && z > 0)
            {
                

                punkty++;
                if (punkty <= 50)
                {
                    punkty++;
                }
                else
                    punkty = 50;

                text.text = "Wylosowano procę";
            }


            if (x == 270 && z == 0)
            {

                punkty++;
                if (punkty <= 30)
                {
                    punkty++;

                }
                else
                    punkty = 50;
                text.text = "Wylosowano koło";
            }


            text2.text = "Punkty: " + punkty;
       

            if (timer > 10)
            {
                if (Input.GetMouseButtonDown(0))
                    SceneManager.LoadScene("Arena");   
            }
        }
        else
        {
            if (timer % 5 >= 0.1f)
                text.text = "Losowanie" + kropka;
            if (timer % 5 >= 0.2f)
                text.text = "Losowanie" + kropka + kropka;
            if (timer % 5 >= 0.3f)
                text.text = "Losowanie" + kropka + kropka + kropka;
        }
    }
}
