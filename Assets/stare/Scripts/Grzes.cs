using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grzes : MonoBehaviour
{

    [SerializeField] Text punkty;
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
  


    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    float points;

    Vector2 startPosition;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    void Start()
    {

        startPosition = rigidbody2D.position;
        rigidbody2D.isKinematic = true;
        rigidbody2D.freezeRotation = true;
        punkty.text = "Punkty: " + points.ToString();

    }

    void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = rigidbody2D.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();

        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(direction * _launchForce);


        spriteRenderer.color = Color.white;
        rigidbody2D.freezeRotation = false;
    }


    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, startPosition);

        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + direction * _maxDragDistance;
        }

        if (desiredPosition.x > startPosition.x)
        {
            desiredPosition.x = startPosition.x;
        }


        rigidbody2D.position = desiredPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        points += 100;
        punkty.text = "Punkty: " + points.ToString();
        StartCoroutine(ResetAfterDelay());


    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        rigidbody2D.position = startPosition;
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.rotation = 0;
      
    }




}
