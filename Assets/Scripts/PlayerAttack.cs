using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    public static bool isAttacking = false;


    public AudioSource meeleeSound1;


    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayers;


    Animator playerAnimator;


    // Use this for initialization
    void Start()
    {

        playerAnimator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        //check if the attackrate allows the next attack
        if (Time.time >= nextAttackTime)
        {

            isAttacking = false;
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                isAttacking = true;
                nextAttackTime = Time.time + 1f / attackRate;
            }


        }



    }//Update

    void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        meeleeSound1.Play();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            print(enemy+"Got hit");
            enemy.GetComponent<Enemy>().TakeDamage(20);
        }

    }


    //Draw a sphere to see the attack Range
    private void OnDrawGizmosSelected()
    {


        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}