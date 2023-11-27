using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public float jumpAmount = 35;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
    public float moveAmount = 0.01f;
    public float buttonTime = 0.3f;
    public float cancelRate = 100;
    public float dashSpeed = 2;
    public int maxJumpTimes = 1;
    public float startDashTime = 10;
    int jumpTimes;
    float currentDashTime;
    private float moveDirection;
    bool jumping = false;
    bool jumpCancelled = false;
    bool canDash = true;
    bool canMove = true;
    public  bool tracked = false;
    float jumpTime = 0;






    public Animator animator;


    private void Start()
    {
        jumpTimes = maxJumpTimes;
    }
    void Update()
    {

        moveDirection = UserInput.instance.playerControls.Player.Move.ReadValue<float>();
        if (UserInput.instance.playerControls.Player.Jump.WasPerformedThisFrame() && grounded)
        {
            rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;

        }
        if(rb.velocity.y < 0.2)
        {
            animator.SetBool("Grounded?D", grounded);
        }
        else
        {
            animator.SetBool("Grounded?D", true);
        }

        if (rb.velocity.y >= 0.2)
        {
            animator.SetBool("Grounded?U", grounded);
        }
        else
        {
            animator.SetBool("Grounded?U", true);
        }


        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (UserInput.instance.playerControls.Player.Jump.WasReleasedThisFrame())
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravityScale;
        }
        if(rb.velocity.y == 0)
        {
            jumpTimes = maxJumpTimes;
        }


        if (UserInput.instance.playerControls.Player.Dash.WasPerformedThisFrame() && canDash)
        {
            if(moveDirection < 0 || sprite.flipX == true)
            {
                StartCoroutine(Dash(Vector2.left));
            }
            else if(moveDirection > 0 || sprite.flipX == false)
            {
                StartCoroutine(Dash(Vector2.right));
            }
        }
        if(grounded && canDash == false)
        {
            canDash = true;
        }

        if (moveDirection > 0)
        {
            sprite.flipX = false;
        }
        if (moveDirection < 0)
        {
            sprite.flipX = true;
        }

    }

    void FixedUpdate()
    {

        if (jumpCancelled && jumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate * Time.deltaTime);
        }


        if (canMove)
        {
        rb.transform.Translate(new Vector2(moveDirection * moveAmount * Time.deltaTime,0));
            animator.SetFloat("Speed", Mathf.Abs(moveDirection) * moveAmount);            
        }


           



    }
    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        canMove = false;
        currentDashTime = startDashTime;

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;

            rb.velocity = direction * dashSpeed;
                                                 

            yield return null;
        }

        rb.velocity = new Vector2(0f, 0f);

        canMove = true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "platform")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tracker")
        {
            tracked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tracker")
        {
            tracked = false;
        }
    }

}
