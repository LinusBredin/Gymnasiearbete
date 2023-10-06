using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    bool jumping = false;
    bool jumpCancelled = false;
    bool canDash = true;
    bool canMove = true;
    bool grounded = true;
    float jumpTime = 0;

    public Animator animator;


    private void Start()
    {
        jumpTimes = maxJumpTimes;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;

        }

        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
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


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            if(Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(Vector2.left));
            }
            else if(Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(Vector2.right));
            }
            else if (sprite.flipX == true)
            {
                StartCoroutine(Dash(Vector2.left));
            }
            else
            {
                StartCoroutine(Dash(Vector2.right));
            }
        }
        if(grounded && canDash == false)
        {
            canDash = true;
        }

    }

    void FixedUpdate()
    {

        if (jumpCancelled && jumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
        if (Input.GetKey(KeyCode.D) && canMove)
        {
            rb.transform.Translate(Vector2.right * moveAmount * Time.deltaTime);
            sprite.flipX = false;
            animator.SetFloat("Speed", moveAmount);
        }
        else if (Input.GetKey(KeyCode.A) && canMove)
        {
            rb.transform.Translate(Vector2.left * moveAmount * Time.deltaTime);
            sprite.flipX = true;
            animator.SetFloat("Speed", moveAmount);
        }
        else
        {
            animator.SetFloat("Speed", 0);
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
}
