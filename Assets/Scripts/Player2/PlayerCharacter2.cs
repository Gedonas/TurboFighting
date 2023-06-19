using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter2 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float crouchSpeed;
    public float crouchScale;
    public float downSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isCrouching = false;
    private bool isJumping = false;
    private bool isAttacking = false;
    private float attackTime = 0f;

    private bool isBlockAnimationPlaying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0f;

        // Check for left and right movement keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1f;
        }

        if (isCrouching && !isBlockAnimationPlaying)
        {
            rb.velocity = new Vector2(moveHorizontal * crouchSpeed, rb.velocity.y);
        }
        else if (!isBlockAnimationPlaying)
        {
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        }

        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetBool("IsRunning", true);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
            rb.velocity += Vector2.down * downSpeed;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) && !isAttacking && isJumping)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("IsDownAttack", true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = true;
            anim.SetBool("IsCrouching", true);
            rb.velocity = Vector2.zero;
            transform.localScale = new Vector3(1f, crouchScale, 1f);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
            anim.SetBool("IsCrouching", false);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            attackTime = 0f;
            anim.SetBool("IsPunch", true);
        }
        if (anim.GetBool("IsPunch") && attackTime >= 0.5f)
        {
            anim.SetBool("IsPunch", false);
        }
        attackTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            anim.SetBool("IsBlock", true);
            isBlockAnimationPlaying = true;
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            anim.SetBool("IsBlock", false);
            isBlockAnimationPlaying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("IsDownAttack", false);
            anim.SetBool("Jump", false);
            isJumping = false;
        }
    }
    public bool IsBlocking()
    {
        return isBlockAnimationPlaying;
    }
}
