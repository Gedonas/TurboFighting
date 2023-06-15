using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        int playerPrefabIndex = GetPlayerPrefabIndex();

        GameObject playerToSpawn = playerPrefabs[playerPrefabIndex];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }

    private int GetPlayerPrefabIndex()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        int prefabIndex = playerCount - 1; // Default to the last prefab

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerAvatar"))
        {
            int selectedAvatar = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"];
            prefabIndex = Mathf.Clamp(selectedAvatar, 0, playerPrefabs.Length - 1);
        }

        return prefabIndex;
    }
}