using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; //Публичная переменная для слайдера ХП
    public int maxHealth; //Публичаня переменная для максимального ХП
    public int currentHealth; //Публичная переменная для текущего ХП
    public GameObject GoDie; //Публияная переменная для отображения "окна смерти"
    public GameObject MainPlayer; //Публичная переменая для указания go_player
    public Animator playerAnimator; // Публичная переменная для компонента аниматора игрока

    private bool isTakingDamage = false; //Флаг для получения урона
    private bool isDead = false; // Флаг для проверки состояния смерти игрока

    void Start()
    {
        currentHealth = maxHealth; //Текущее ХП соответствует максимальному
        healthSlider.maxValue = maxHealth; //Текущее масимальное ХП отображается на слайдер
        healthSlider.value = currentHealth; //Текущее ХП отображается на слайдер
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
        playerAnimator.SetTrigger("Die"); // Запускаем анимацию смерти
        yield return new WaitForSeconds(1.0f); // Ждем 1 секунду для завершения анимации
        GoDie.SetActive(true); // Включаем "окно смерти"
    }
}
