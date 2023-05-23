using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float crouchSpeed; // скорость передвижения в приседе
    public float crouchScale; // масштаб при приседании
    private Rigidbody2D rb;
    private Animator anim;
    private bool isCrouching = false; // флаг состояния приседания
    private bool isJumping = false; // флаг состояния прыжка

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
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // Добавлено условие для проверки состояния прыжка
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            isJumping = true; // Установка флага прыжка в true
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
            anim.SetBool("IsCrouching", true);
            rb.velocity = Vector2.zero; // остановка персонажа при приседании
            transform.localScale = new Vector3(1f, crouchScale, 1f); // изменение масштаба при приседании
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
            anim.SetBool("IsCrouching", false);
            transform.localScale = new Vector3(1f, 1f, 1f); // возвращаем обычный масштаб при вставании
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Jump", false);
            isJumping = false; // Сброс флага прыжка при касании земли
        }
    }
}