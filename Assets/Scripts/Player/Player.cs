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







    // Use this for initialization
    void Start()
    {



        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerAttack.isAttacking)
        {
            horizontal = 0;
        }
        else
        {
            // Get input to variables
            horizontal = Input.GetAxis("Horizontal");
            speed = Mathf.Abs(horizontal);


        }


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

    

    }

    void Update()
    {




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

            if (jumpTimeCounter > 0)
            {

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

    public void GiveSuperJumps()
    {
        jumpForce = 2.3f;
    }
}
