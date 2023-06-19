using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab1; // Префаб первого игрока
    public GameObject playerPrefab2; // Префаб второго игрока
    public Transform spawnPoint1; // Точка спавна первого игрока
    public Transform spawnPoint2; // Точка спавна второго игрока

    private void Start()
    {
        Time.timeScale = 1.0f;
        // Создаем первого игрока
        GameObject player1 = Instantiate(playerPrefab1, spawnPoint1.position, Quaternion.identity);

        // Создаем второго игрока
        GameObject player2 = Instantiate(playerPrefab2, spawnPoint2.position, Quaternion.identity);
    }
}
