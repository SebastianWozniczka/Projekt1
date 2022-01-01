using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    private Rigidbody2D m_body2d;
    public Rigidbody2D playerRigidbody;
    private Sensor_Bandit m_groundSensor;
    public LayerMask playerLayers;

    private float inputX;
    readonly float m_speed = 0.5f;
    readonly float m_jumpForce = 7.5f;
    public int maxHealth = 100;
   int currentHealth;
    private bool m_grounded = false;
    private float timerJump;
    private float timerAttack;
    private bool grounded;
    public float attackRange = 2f;
    public int attackDamage = 40;
    private bool collide=false;

    void Start()
    {
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        currentHealth = maxHealth;
        timerJump = 0;
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    public void Update()
    {


        //time
        timerJump += Time.deltaTime;
         timerAttack += Time.deltaTime;
      


        
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            animator.SetInteger("AnimState", 2);



        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            animator.SetBool("Grounded", m_grounded);
        }



        if (timerJump>=5) {

            jump();

        }
         void jump()
        {
          
            animator.SetTrigger("Jump");
            m_grounded = false;
            animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            timerJump = 0;
        }

        if (timerAttack > 3)
        {
            animator.SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Bandit>().TakeDamage(attackDamage);
            }

            timerAttack = 0;
        }

        if (playerRigidbody.position.x > m_body2d.position.x)
        {
            inputX = 5;
        }

        if (playerRigidbody.position.x < m_body2d.position.x)
        {
            inputX = -5;
        }
       if(playerRigidbody.position.x == m_body2d.position.x)
        {
            jump();
            if(inputX > 0)
            inputX = 15;
            else inputX = -15;

        }
           
        animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX <= 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
       
        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

    }

    public void TakeDamage(int damage)
    {
         animator.SetTrigger("Hurt");
         currentHealth -= damage;
       
        if (currentHealth <= 0)
         Die();
       
    }

    void Die()
    {
        animator.SetTrigger("Death");
        this.GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name== "HeavyBandit")
        {
            collide = true;
        }

        if (collision.gameObject.name == "box1")
        {

            inputX = -inputX;

        }


    }

}

