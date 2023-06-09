using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab1; // ������ ������� ������
    public GameObject playerPrefab2; // ������ ������� ������
    public Transform spawnPoint1; // ����� ������ ������� ������
    public Transform spawnPoint2; // ����� ������ ������� ������

    private void Start()
    {
        // ������� ������� ������
        GameObject player1 = Instantiate(playerPrefab1, spawnPoint1.position, Quaternion.identity);

        // ������� ������� ������
        GameObject player2 = Instantiate(playerPrefab2, spawnPoint2.position, Quaternion.identity);
    }
}
