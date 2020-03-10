using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float attackRate = 1.5f;
    public static float nextAttackTime;
    float distToPlayer;

    public Transform attackPoint;
    public float attackRadius;
    public LayerMask playerLayer;

    Rigidbody2D rb;
    Transform player;

    [SerializeField]
    bool facingRight = true;
    Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    bool isGrounded;



    bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()//-------------------------------------------------
    {
        //Recognize when Enemy has attacked, and set nextAttackTime
        if(animator.GetBool("Attack")==true){
            
            nextAttackTime = Time.time + 1f / attackRate;
        }

        //Look at Player
        if (transform.position.x > player.position.x && facingRight)
        {
            enemyFlip();
        }
        else if (transform.position.x < player.position.x && !facingRight)
        {
            enemyFlip();
        }

        distToPlayer = Mathf.Abs(rb.position.x- player.position.x);
        animator.SetFloat("distToPlayer", distToPlayer);

       /*  if(isGrounded){
            rb.bodyType = RigidbodyType2D.Kinematic;
            print("Kinematic");
        }else  */if(!isGrounded){
            rb.bodyType = RigidbodyType2D.Dynamic;
            print("Dynamic");
        }


    }//----------------------END: Update -----------------------------------

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (!isDead)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        //transform.position = transform.position - new Vector3(0, 0.06f, 0);
        animator.SetBool("isDead", true);

        //dont move when dead
        rb.velocity = new Vector2(0f, 0f);
        rb.angularVelocity = 0f;

        rb.mass = 0;
        rb.gravityScale = 0;

        //Disable all coliders when dead
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
        
        //Disable Script after colliders (otherwise coliders dont get disabled)
        this.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag =="Ground"){
            isGrounded = true;
        }else{
            isGrounded = false;
        }
    }

    void enemyFlip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    public void DealDamage(int damage)
    {
        Collider2D[] thatGotHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        if (thatGotHit.Length > 0)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }




}
