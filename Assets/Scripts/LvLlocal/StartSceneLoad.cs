using Photon.Pun.Demo.Procedural;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneLoad : MonoBehaviour
{
    public GameObject block;
    private void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Countdown());
    }
    public Text timerText;

    private int timeLeft = 3;

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        block.SetActive(false);
        timerText.text = "Go!";
        yield return new WaitForSeconds(1);

        timerText.gameObject.SetActive(false); // Опционально: скрыть текст после окончания отсчета
    }
}
