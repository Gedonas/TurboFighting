using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float crouchSpeed;
    public float crouchScale;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isCrouching = false;
    private bool isJumping = false;
    private bool isAttacking = false;
    private float attackTime = 0f;
    public GameObject goWin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (isCrouching)
        {
            rb.velocity = new Vector2(moveHorizontal * crouchSpeed, rb.velocity.y);
        }
        else
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            if (isJumping)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("IsDownAttack", true);
            }
            else
            {
                anim.SetBool("IsPunch", true);
            }

            isAttacking = true;
            attackTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
            anim.SetBool("IsCrouching", true);
            rb.velocity = Vector2.zero;
            transform.localScale = new Vector3(1f, crouchScale, 1f);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
            anim.SetBool("IsCrouching", false);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (isAttacking && attackTime >= 0.5f)
        {
            if (isJumping)
            {
                anim.SetBool("IsDownAttack", false);
            }
            else
            {
                anim.SetBool("IsPunch", false);
            }

            isAttacking = false;
        }

        attackTime += Time.deltaTime;
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
}
