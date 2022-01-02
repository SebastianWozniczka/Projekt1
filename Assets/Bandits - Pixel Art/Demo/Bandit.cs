using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Bandit : MonoBehaviour {

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Button attackButton, leftButton, rightButton,upButton,blockButton;
    public Camera cam;
    public Rigidbody2D enemyRigidBody;
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;

    public float x;
    public float attackRange = 1.2f;
    private bool block = false;
    public int attackDamage = 40;
    private bool atk = false;
    private bool mLeft = false;
    private bool mRight = false;
    private bool mUp = false;
    private bool mBlock = false;
    private bool hitBlock = false;
    private float inputX;
    public int maxHealth = 100;
    int currentHealth;
    private float hitTimer;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
   
    public bool facingDirection;

    // Use this for initialization
    void Start () {

        Click();
       

        facingDirection = false;
        currentHealth = maxHealth;
        hitTimer = 0;
        

    }


    void Click()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        attackButton.onClick.AddListener(PlayerAttack);
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
        upButton.onClick.AddListener(MoveUp);
        blockButton.onClick.AddListener(Block1);
    }


    // Update is called once per frame
    void Update () {

        x = enemyRigidBody.position.x;

        if (mLeft)
        {
            inputX = -1;

            mLeft = false;
        }


        if (mRight)
        {
            m_body2d.velocity = new Vector2(5, 0);
            inputX = 1;
             mRight = false;
        }

        
        if (!m_grounded ) { 
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded) {
            m_animator.SetBool("Grounded", m_grounded);
        }

      

        // Swap direction of sprite depending on walk direction
        if ((inputX < 0 && facingDirection) || (inputX > 0 && !facingDirection))
        {
            facingDirection = !facingDirection;
            transform.Rotate(new Vector3(0, 180, 0));
        }

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

       

        //Attack
        if (atk&&!hitBlock)
        {

                m_animator.SetTrigger("Attack");

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {

                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

                  
                }
                hitBlock = true;
          
            atk = false;
        }

        if (hitBlock)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer > 1)
            {
                hitBlock = false;
                hitTimer = 0;
            }
        }


        //Change between idle and combat idle
        if (mBlock)
        {
            inputX = 0;
            m_combatIdle = !m_combatIdle;
            mBlock = false;
        }
        //Jump
        else if (mUp && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            mUp = false;
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
        {

            hitBlock = true;
            m_animator.SetInteger("AnimState", 1);

            if (!facingDirection && enemyRigidBody.position.x < m_body2d.position.x)
            {
                block = true;
            }
            if (facingDirection && enemyRigidBody.position.x > m_body2d.position.x)
            {
                block = true;
            }
            
        }

        //Idle
        else
        {
            m_animator.SetInteger("AnimState", 0);
            block = false;
           
        }
    }
    public void TakeDamage(int damage)
    {

        if (!block)
        {
            m_animator.SetTrigger("Hurt");
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
               // Die();
            }
        }
    }

    void Die()
    {
        m_animator.SetTrigger("Death");
        this.GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }


    public void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ziemia")
        {
            m_grounded = true;
        }

        if (collision.gameObject.name == "box1")
        {
           
            inputX = -inputX;
           

        }

        if (collision.gameObject.name == " LightBandit")
        {
            m_grounded = false;
        }


    }

    
    void PlayerAttack()
    {
        if(!hitBlock)
        atk = true;
    }

    void MoveLeft()
    {
       mLeft = true;
    }

    void MoveRight()
    {
        mRight = true;
    }

    void MoveUp()
    {
        mUp = true;

    }

    void Block1()
    {
        mBlock = true;

    }

}
