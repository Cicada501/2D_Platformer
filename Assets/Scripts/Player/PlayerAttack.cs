using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float startFreezingTime = 0.1f;
    public float endFreezingTime = 0.2f;
    bool enemyInDamagezone = false;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    public static bool isAttacking = false;

    float timeForAnimPause = 0f;
    float timeForAnimResume = 0f;


    public AudioSource meeleeSound1;
    public AudioSource hitmarkerSound;


    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;


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
                timeForAnimPause = Time.time + startFreezingTime; //when to start freeze 
                timeForAnimResume = Time.time + endFreezingTime; //when to end freeze
            }


        }
        //if player hits an enemy, interrupt animation for a short time
        if (enemyInDamagezone)
        {
            //happens first
            if (Time.time >= timeForAnimPause && Time.time <= timeForAnimResume)
            {
                playerAnimator.enabled = false;

            }
            //happens afterwards
            else if (Time.time >= timeForAnimResume)
            {
                playerAnimator.enabled = true;
                enemyInDamagezone = false;

            }
        }


    }//Update

    void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        meeleeSound1.Play();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
        if (hitEnemies.Length > 0)
        {
            enemyInDamagezone = true;
            hitmarkerSound.Play();


            foreach (Collider2D enemy in hitEnemies)
            {
                print(enemy + "Got hit");
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
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