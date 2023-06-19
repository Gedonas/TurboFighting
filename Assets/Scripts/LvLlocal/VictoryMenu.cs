using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
  
    public int indexScene;
    public GameObject victoryMenu; // Публичная переменная для экрана победы
    public Image winnerImage; // Публичная переменная для изображения победителя

    public GameObject player1Sprite; // Спрайт для игрока 1
    public GameObject player2Sprite; // Спрайт для игрока 2

    public GameObject PauseGame;


    public void ShowVictoryMenu(GameObject winningPlayer)
    {
        victoryMenu.SetActive(true); // Включаем экран победы

        // Определяем, кто победил
        if (winningPlayer.CompareTag("Player1"))
        {
            player2Sprite.SetActive(true);
            PauseGame.SetActive(false);
        }
        else if (winningPlayer.CompareTag("Player2"))
        {
            player1Sprite.SetActive(true);
            PauseGame.SetActive(false);
        }
    }

    public void ContinionGame()
    {
        Time.timeScale = 1.0f;
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(indexScene);
    }
}
