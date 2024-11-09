using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyWaves;
    RoomData _roomData;
    int _currentWaveID = 0;
    int _enemyCount = 0;
    float _waveTimeDelay = 1.0f;
    bool isActiveRoom = false; // needs implemented

    private void OnEnable()
    {
        Callbacks.EnemyKilled += OnEnemyKilled;
        Callbacks.EnemySpawned += OnNewEnemyCreated;
        Callbacks.WaveCompleted += OnWaveComplete;
        Callbacks.RoomGenerationComplete += BeginRoom; //debug
    }

    private void OnDisable()
    {
        Callbacks.EnemyKilled -= OnEnemyKilled;
        Callbacks.EnemySpawned -= OnNewEnemyCreated;
        Callbacks.WaveCompleted -= OnWaveComplete;
        Callbacks.RoomGenerationComplete -= BeginRoom; //debug

    }

    void Start()
    {
        _roomData = GetComponent<RoomData>();
    }

    void BeginRoom() //will be called once a player has entered a room.
    {
        isActiveRoom = true;
        StartCoroutine(CoroutineInitialiseWave());
    }

    private void OnWaveComplete()
    {
        _enemyWaves[_currentWaveID].SetActive(false);
        _currentWaveID++;
        if(_currentWaveID == _enemyWaves.Length) //if the final wave is complete
        {
            Callbacks.CallEvent(CallbackEvent.RoomCompleted);
        }
        StartCoroutine(CoroutineInitialiseWave());
    }

    IEnumerator CoroutineInitialiseWave()
    {
        yield return new WaitForSeconds(_waveTimeDelay);
        GameObject newWave = _enemyWaves[_currentWaveID];
        newWave.SetActive(true);

        foreach (Transform child in newWave.transform) //each child of the wave
        {
            if (child.CompareTag("Enemy"))
            {
                _enemyCount++;
            }
        }

    }

    void OnEnemyKilled()
    {
        _enemyCount--;
        if (_enemyCount < 0) Debug.LogError("Negative Enemy count, something has gone wrong.");
        if (_enemyCount == 0)
        {
            Callbacks.CallEvent(CallbackEvent.WaveCompleted);
        }
    }

    void OnNewEnemyCreated()
    {
        _enemyCount++;
    }

    void OnRoomEntered()
    {
        //close the opening door, then begin the room.
    }

    void OnRoomExited()
    {
        //close the door, then disable this room.
    }
}
