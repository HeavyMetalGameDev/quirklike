using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomGenerator))]
public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private RoomGenerator roomGenerator;
    private Transform playerSpawnTransform;
    [SerializeField]
    private GameObject playerPrefab;
    private bool hasPlayerSpawned = false;
    void Start()
    {
        roomGenerator = this.GetComponent<RoomGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPlayerSpawned && roomGenerator.HasRoomsGenerated())
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        
        playerSpawnTransform = roomGenerator.GetStartingRoom().GetComponent<RoomData>().GetPlayerSpawn();
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, playerSpawnTransform.position, new Quaternion());
            hasPlayerSpawned = true;
        }
    }
}
