using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; //��������� ���������� ��� �������� ��
    public int maxHealth; //��������� ���������� ��� ������������� ��
    public int currentHealth; //��������� ���������� ��� �������� ��
    public GameObject GoDie; //��������� ���������� ��� ����������� "���� ������"
    public GameObject MainPlayer; //��������� ��������� ��� �������� go_player
    public Animator playerAnimator; // ��������� ���������� ��� ���������� ��������� ������

    private bool isTakingDamage = false; //���� ��� ��������� �����
    private bool isDead = false; // ���� ��� �������� ��������� ������ ������

    void Start()
    {
        currentHealth = maxHealth; //������� �� ������������� �������������
        healthSlider.maxValue = maxHealth; //������� ����������� �� ������������ �� �������
        healthSlider.value = currentHealth; //������� �� ������������ �� �������
    }

    public void OnTriggerEnter2D(Collider2D damageenemy)
    {
        if ((damageenemy.CompareTag("EnemyAttackEasy")) && !isTakingDamage && !isDead)
        {
            isTakingDamage = true;
            currentHealth--;
            healthSlider.value = currentHealth;
            StartCoroutine(ResetTakingDamageFlag());
            if (currentHealth <= 0)
            {
                isDead = true;
                StartCoroutine(PlayDeathAnimation());
            }
        }
    }

    IEnumerator ResetTakingDamageFlag()
    {
        yield return new WaitForSeconds(0.05f);
        isTakingDamage = false;
    }

    IEnumerator PlayDeathAnimation()
    {
        playerAnimator.SetTrigger("Die"); // ��������� �������� ������
        yield return new WaitForSeconds(1.0f); // ���� 1 ������� ��� ���������� ��������
        GoDie.SetActive(true); // �������� "���� ������"
    }
}
