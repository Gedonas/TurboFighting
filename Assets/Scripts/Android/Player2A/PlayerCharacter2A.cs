using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCharacter2A : MonoBehaviour
{
    public GameObject player; //публичная переменная чтобы в Unity Editor указать игрока
    public float speedPlayerY; //публичная переменная чтобы указать скорость игрока по оси Y

    private bool leftMove; //приватная переменная для активации движения влево
    private bool rightMove; //приватная переменная для активации движения вправо
    private Rigidbody2D rb;

    public float jumpforse;//публичная переменная для указания силы прыжка
    public int numberjumps; //публичная переменная для указания количество непрерывных прыжков
    private int countJump; //приватная перемення чтобы скрипт мог считать сколько доступно прыжков

    public Transform chekGround; //публичная переменная чтобы указать точку в игроке, когда должен соприкасаться с поверхностью
    public LayerMask whatGround; //публичная переменная, чтобы узанать что мы считаем за землю
    public float chekRadius; //публичная переменная, чтобы узнать радиус который мы будет проверять
    private bool isGround; //приватная переменная, чтобы определять соприкасается ли игрок с землей
    public Animator Anim; //публичная переменная чтобы указать анимацию
    private bool isJumping = false;

    private bool isAttacking; // приватная переменная для отслеживания состояния атаки
    private Coroutine attackCoroutine; // приватная переменная для хранения ссылки на корутину атаки

    private bool isBlockAnimationPlaying = false;


    private void Start() //приватная функция при запуске сцены
    {
        rb = GetComponent<Rigidbody2D>();

        Anim.SetBool("IsRunning", false); //булевская переменная, которая отключает анимацию бега при старте : то есть по умолчанию проигрывается анимация IDLE
        leftMove = false; //при старте движение влево не активно
        rightMove = false; //при старте движение вправо не активно
        countJump = numberjumps; //при старке игрок получает доступное ему количество прыжков
    }

    private void Update() //фунция для обновления кадров со стороны телефона
    {
        if (isGround == true) //условие верное для соприкосновения игрока и земли
        {
            countJump = numberjumps; //игроку доступко количество доступных прыжков в реальном времени
        }
    }

    public void DownLeftMove() //публичная фунция движения влево, когда нажимается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsRunning", true); //булевская переменная, которая включает анимацию бега
        player.transform.localScale = new Vector3(-1f, 1f, 1f); //указывается в каком направлении будет игрок
        leftMove = true; //активация движения влево
    }

    public void UpLeftMove() //публичная фунция движения влево, когда отпускается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsRunning", false); //булевская переменная, которая отключает анимацию бега
        leftMove = false; //невозможно движения влево
    }

    public void DownRightMove() //публичная фунция движения вправо, когда нажимается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsRunning", true); //булевская переменная, которая включает анимацию бега
        player.transform.localScale = new Vector3(1f, 1f, 1f); //указывается в каком направлении будет игрок
        rightMove = true; //активация движения вправо
    }

    public void UpRightMove() //публичная фунция движения вправо, когда отпускается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsRunning", false); //булевская переменная, которая отключает анимацию бега
        rightMove = false; //невозможно движения влево
    }

    public void DownBlock() //публичная фунция движения вправо, когда нажимается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsBlock", true); //булевская переменная, которая включает анимацию бега
        player.transform.localScale = new Vector3(1f, 1f, 1f); //указывается в каком направлении будет игрок
        isBlockAnimationPlaying = true;
    }

    public void UpBlock() //публичная фунция движения вправо, когда отпускается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsBlock", false); //булевская переменная, которая отключает анимацию бега
        isBlockAnimationPlaying = false;
    }

    public void DownDown() //публичная фунция движения вправо, когда нажимается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsCrouching", true); //булевская переменная, которая включает анимацию бега
        player.transform.localScale = new Vector3(1f, 1f, 1f); //указывается в каком направлении будет игрок
    }

    public void UpDown() //публичная фунция движения вправо, когда отпускается кнопка с компонентом Event Triggner
    {
        Anim.SetBool("IsCrouching", false); //булевская переменная, которая отключает анимацию бега
    }

    public void Jump() //публичная функиця для прыжка
    {
        rb.AddForce(new Vector2(0f, jumpforse), ForceMode2D.Impulse);
        Anim.SetBool("Jump", true);
        isJumping = true;
    }

    public void PlayAttack()
    {
        if (!isAttacking) // проверяем, не выполняется ли уже атака
        {
            Anim.SetBool("IsPunch", true); // проигрываем атаку
            isAttacking = true; // устанавливаем флаг, что атака выполняется

            if (attackCoroutine != null) // если предыдущая корутина атаки существует, останавливаем ее
            {
                StopCoroutine(attackCoroutine);
            }

            attackCoroutine = StartCoroutine(AttackDelay()); // запускаем корутину с задержкой атаки
        }
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.55f); // задержка в 0.3 секунды

        Anim.SetBool("IsPunch", false); // завершаем атаку
        isAttacking = false; // сбрасываем флаг атаки
        attackCoroutine = null; // сбрасываем ссылку на корутину
    }

    private void FixedUpdate() //приватная функция Update
    {
        if (leftMove) //условие если активируется движение влево
        {
            player.transform.position += new Vector3(-speedPlayerY * Time.deltaTime, 0, 0); //происходит смена координат с помощью отрицания скорости
        }
        else if (rightMove) //условие если активируется движение вправо
        {
            player.transform.position += new Vector3(speedPlayerY * Time.deltaTime, 0, 0); //происходит смена координат с помощью скорости
        }
        isGround = Physics2D.OverlapCircle(chekGround.position, chekRadius, whatGround); //принятия точки, радиус вокрук точки, то что мы считаем за землю
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