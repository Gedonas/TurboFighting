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

        if (Input.GetKeyDown(KeyCode.J))
        {
            attackTime = 0f; // Сбросить время, прошедшее с начала атаки
            anim.SetBool("IsPunch", true);
        }

        // Если анимация атаки активна и прошла 1 секунда, остановить анимацию
        if (anim.GetBool("IsPunch") && attackTime >= 0.5f)
        {
            anim.SetBool("IsPunch", false);
        }

        attackTime += Time.deltaTime; // Увеличить время, прошедшее после нажатия клавиши J
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Jump", false);
            isJumping = false;
        }
    }
}
