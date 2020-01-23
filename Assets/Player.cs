using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour
{
    bool facingRight = true;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator playerAnimator;

    [SerializeField]
    Animator backgroundAnimator;
    [SerializeField]
    Rigidbody2D rb;
    float horizontal;

    [SerializeField]
    float speed;

    //Variables for Jumping
    public static bool isGrunded;
    public static bool isOnPlatform1;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform1;

    public float jumpForce;

    public static bool isFalling = false;
    private bool isJumping = false;
    public float jumpTime;
    private float jumpTimeCounter;



    bool landing = false;

    //Attack
    bool attackMode = false;
    bool attack1;
    bool attack2;
    bool attack3;

    float timeBtwAttack;
    public float startTimeBtwAttack;

    public float startAttack1Duration;
    public float startAttack2Duration;
    public float startAttack3Duration;
    float attackDuration1 = 0;
    float attackDuration2 = 0;
    float attackDuration3 = 0;

    bool attack1Used = false;
    bool attack2Used = false;
    bool attack3Used = true;


    int attackConter = 0;





    // Use this for initialization
    void Start()
    {




        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        // Get input to variables
        horizontal = Input.GetAxis("Horizontal");
        speed = Mathf.Abs(horizontal);

        //apply input to player (moveing left, right)
        rb.velocity = new Vector2(horizontal, rb.velocity.y);


        //face player in the right direction
        if (horizontal > 0 && !facingRight)
        {

            flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            flip();
        }




        //Animation
        playerAnimator.SetFloat("Speed", speed);
        playerAnimator.SetBool("isGrounded", (isOnPlatform1 || isGrunded));
        playerAnimator.SetBool("isFalling", isFalling);
        playerAnimator.SetBool("attackMode", attackMode);
        playerAnimator.SetBool("attack1", attack1);
        playerAnimator.SetBool("attack2", attack2);
        playerAnimator.SetBool("attack3", attack3);

    }

    void Update()
    {

        //---------------------ATTACKING--------------------------------

        print("Attack1: " + attack1 + "\tAttack2: " + attack2 + "\tAttack3: " + attack3);


        if (!attack1 && !attack2 && !attack3)
        {
            if (Input.GetMouseButtonDown(0) && !attack2 && !attack3)
            {
                attack1 = true;
            }
        }

        else if (!attack2 && attack1 && !attack3)
        {
            if (Input.GetMouseButtonDown(0) && attack1 && !attack3)
            {
                attack2 = true;
            }
        }
        else if (attack1 && attack2 && !attack3)
        {
            if (Input.GetMouseButtonDown(0) && attack1 && attack2)
            {
                attack3 = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && attack1 && attack2 && attack3)
            {
                attack1 = false;
                attack2 = false;
                attack3 = false;
            }


        }

        /* 		//Do attack1 if 
                if(Input.GetMouseButtonDown(0) && attackDuration<=0  && horizontal == 0 && isGrunded){ // 0 = LMB, 1 = RMB, 

                    attack1 = true;
                    attackMode = true;
                    attackDuration = startAttack1Duration;
                    attackConter++;


                }else if(attackDuration<=0){
                    attack1 = false;
                    attack2 = false;
                    attack3 = false;
                    attackConter = 0;

                }else if(attack1 && attackDuration > 0 && Input.GetMouseButtonDown(0) && attackConter == 1){
                    attack2 = true;
                    attackDuration += startAttack2Duration;
                    attackConter++;

                }else if(attack1 && attack2 && attackDuration > 0 && Input.GetMouseButtonDown(0) && attackConter == 2){
                    attack3 = true;
                    attackDuration += startAttack3Duration;
                    attackConter++;

                }

                else{
                    attackDuration -= Time.deltaTime;
                } */



        //--------------------END: ATTACKING -------------------------------

        //CameraShaker.Instance.ShakeOnce(1f,4f,1f,10f);
        //check if player is on the Ground
        isGrunded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        isOnPlatform1 = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsPlatform1);
        //Jump
        if ((isOnPlatform1 || isGrunded) && Input.GetKeyDown(KeyCode.Space))
        {
            isGrunded = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        //Check if player is falling for the animation
        if (rb.velocity.y < -0.1 && !(isOnPlatform1 || isGrunded))
        {
            isFalling = true;

        }
        else if ((isOnPlatform1 || isGrunded))
        {
            isFalling = false;
        }




        //Longer Jump on Space holding
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            print("Here1");
            if (jumpTimeCounter > 0)
            {
                print("Here2");
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }


    }

    void flip()
    {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }
}
