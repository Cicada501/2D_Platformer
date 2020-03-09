using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        //Look at Player
        if (transform.position.x > player.position.x && facingRight)
        {
            enemyFlip();
        }
        else if (transform.position.x < player.position.x && !facingRight)
        {
            enemyFlip();
        }

        distToPlayer = Vector2.Distance(rb.position, player.position);
		animator.SetFloat("distToPlayer",distToPlayer);


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

        //Disable Collider and script
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
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
