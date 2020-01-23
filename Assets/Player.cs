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

        //Activate attackMode by pressing Q
        if (Input.GetKeyDown(KeyCode.Q) && !attackMode)
        {
            attackMode = true;
            //Deactivate by pressing Q again
        }
        else if (Input.GetKeyDown(KeyCode.Q) && attackMode)
        {
            attackMode = false;
        }
        //Deactivate attackmode when running
        if (horizontal != 0)
        {
            attackMode = false;
        }

        print(attack1Used);
        if (Input.GetMouseButtonDown(0) && attackDuration1 <= 0 && !attack1Used && horizontal == 0 && isGrunded && attack3Used)
        {

            attack1 = true;
            attack1Used = false;
            attackMode = true;
            attackDuration1 = startAttack1Duration;

        }
        else if (attackDuration1 <= 0 && attack1)
        {
            attack1 = false;
            attack1Used = true;

        }
        else
        {
            attackDuration1 -= Time.deltaTime;
        }

        if (attack1Used && Input.GetMouseButtonDown(0) && attackDuration2 <= 0)
        {
            attack2 = true;
            attack2Used = false;
            attack1Used = false;
            attackDuration2 = startAttack2Duration;

        }
        else if (attackDuration2 <= 0 && attack2)
        {
            attack2 = false;
            attack2Used = true;
        }
        else
        {
            attackDuration2 -= Time.deltaTime;
        }

        if (attack2Used && Input.GetMouseButtonDown(0) && attackDuration3 <= 0)
        {
            attack3 = true;
            attack3Used = false;
            attack2Used = false;
            attack1Used = false;
            attackDuration2 = startAttack2Duration;

        }
        else if (attackDuration3 <= 0 && attack3)
        {
            attack3 = false;
            attack3Used = true;
        }
        else
        {
            attackDuration3 -= Time.deltaTime;
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
