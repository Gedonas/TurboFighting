using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCharacter1A : MonoBehaviour
{
    public GameObject player; //��������� ���������� ����� � Unity Editor ������� ������
    public float speedPlayerY; //��������� ���������� ����� ������� �������� ������ �� ��� Y

    private bool leftMove; //��������� ���������� ��� ��������� �������� �����
    private bool rightMove; //��������� ���������� ��� ��������� �������� ������
    private Rigidbody2D rb;

    public float jumpforse;//��������� ���������� ��� �������� ���� ������
    public int numberjumps; //��������� ���������� ��� �������� ���������� ����������� �������
    private int countJump; //��������� ��������� ����� ������ ��� ������� ������� �������� �������

    public Transform chekGround; //��������� ���������� ����� ������� ����� � ������, ����� ������ ������������� � ������������
    public LayerMask whatGround; //��������� ����������, ����� ������� ��� �� ������� �� �����
    public float chekRadius; //��������� ����������, ����� ������ ������ ������� �� ����� ���������
    private bool isGround; //��������� ����������, ����� ���������� ������������� �� ����� � ������
    public Animator Anim; //��������� ���������� ����� ������� ��������
    private bool isJumping = false;

    private bool isAttacking; // ��������� ���������� ��� ������������ ��������� �����
    private Coroutine attackCoroutine; // ��������� ���������� ��� �������� ������ �� �������� �����

    private bool isBlockAnimationPlaying = false;


    private void Start() //��������� ������� ��� ������� �����
    {
        rb = GetComponent<Rigidbody2D>();

        Anim.SetBool("IsRunning", false); //��������� ����������, ������� ��������� �������� ���� ��� ������ : �� ���� �� ��������� ������������� �������� IDLE
        leftMove = false; //��� ������ �������� ����� �� �������
        rightMove = false; //��� ������ �������� ������ �� �������
        countJump = numberjumps; //��� ������ ����� �������� ��������� ��� ���������� �������
    }

    private void Update() //������ ��� ���������� ������ �� ������� ��������
    {
        if (isGround == true) //������� ������ ��� ��������������� ������ � �����
        {
            countJump = numberjumps; //������ �������� ���������� ��������� ������� � �������� �������
        }
    }

    public void DownLeftMove() //��������� ������ �������� �����, ����� ���������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsRunning", true); //��������� ����������, ������� �������� �������� ����
        player.transform.localScale = new Vector3(-1f, 1f, 1f); //����������� � ����� ����������� ����� �����
        leftMove = true; //��������� �������� �����
    }

    public void UpLeftMove() //��������� ������ �������� �����, ����� ����������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsRunning", false); //��������� ����������, ������� ��������� �������� ����
        leftMove = false; //���������� �������� �����
    }

    public void DownRightMove() //��������� ������ �������� ������, ����� ���������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsRunning", true); //��������� ����������, ������� �������� �������� ����
        player.transform.localScale = new Vector3(1f, 1f, 1f); //����������� � ����� ����������� ����� �����
        rightMove = true; //��������� �������� ������
    }

    public void UpRightMove() //��������� ������ �������� ������, ����� ����������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsRunning", false); //��������� ����������, ������� ��������� �������� ����
        rightMove = false; //���������� �������� �����
    }

    public void DownBlock() //��������� ������ �������� ������, ����� ���������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsBlock", true); //��������� ����������, ������� �������� �������� ����
        player.transform.localScale = new Vector3(1f, 1f, 1f); //����������� � ����� ����������� ����� �����
        isBlockAnimationPlaying = true;
    }

    public void UpBlock() //��������� ������ �������� ������, ����� ����������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsBlock", false); //��������� ����������, ������� ��������� �������� ����
        isBlockAnimationPlaying = false;
    }

    public void DownDown() //��������� ������ �������� ������, ����� ���������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsCrouching", true); //��������� ����������, ������� �������� �������� ����
        player.transform.localScale = new Vector3(1f, 1f, 1f); //����������� � ����� ����������� ����� �����
    }

    public void UpDown() //��������� ������ �������� ������, ����� ����������� ������ � ����������� Event Triggner
    {
        Anim.SetBool("IsCrouching", false); //��������� ����������, ������� ��������� �������� ����
    }

    public void Jump() //��������� ������� ��� ������
    {
        rb.AddForce(new Vector2(0f, jumpforse), ForceMode2D.Impulse);
        Anim.SetBool("Jump", true);
        isJumping = true;
    }

    public void PlayAttack()
    {
        if (!isAttacking) // ���������, �� ����������� �� ��� �����
        {
            Anim.SetBool("IsPunch", true); // ����������� �����
            isAttacking = true; // ������������� ����, ��� ����� �����������

            if (attackCoroutine != null) // ���� ���������� �������� ����� ����������, ������������� ��
            {
                StopCoroutine(attackCoroutine);
            }

            attackCoroutine = StartCoroutine(AttackDelay()); // ��������� �������� � ��������� �����
        }
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.55f); // �������� � 0.3 �������

        Anim.SetBool("IsPunch", false); // ��������� �����
        isAttacking = false; // ���������� ���� �����
        attackCoroutine = null; // ���������� ������ �� ��������
    }

    private void FixedUpdate() //��������� ������� Update
    {
        if (leftMove) //������� ���� ������������ �������� �����
        {
            player.transform.position += new Vector3(-speedPlayerY * Time.deltaTime, 0, 0); //���������� ����� ��������� � ������� ��������� ��������
        }
        else if (rightMove) //������� ���� ������������ �������� ������
        {
            player.transform.position += new Vector3(speedPlayerY * Time.deltaTime, 0, 0); //���������� ����� ��������� � ������� ��������
        }
        isGround = Physics2D.OverlapCircle(chekGround.position, chekRadius, whatGround); //�������� �����, ������ ������ �����, �� ��� �� ������� �� �����
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Anim.SetBool("IsDownAttack", false);
            Anim.SetBool("Jump", false);
            isJumping = false;
        }
    }
    public bool IsBlocking()
    {
        return isBlockAnimationPlaying;
    }
}