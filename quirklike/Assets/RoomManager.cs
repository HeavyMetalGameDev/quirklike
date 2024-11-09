using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyWaves;
    RoomTrigger[] _roomTriggers;
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
        Callbacks.RoomEntered += OnRoomEntered;
        Callbacks.RoomExited += OnRoomExited;
    }

    private void OnDisable()
    {
        Callbacks.EnemyKilled -= OnEnemyKilled;
        Callbacks.EnemySpawned -= OnNewEnemyCreated;
        Callbacks.WaveCompleted -= OnWaveComplete;
        Callbacks.RoomEntered -= OnRoomEntered;
        Callbacks.RoomExited -= OnRoomExited;
    }

    private void Awake()
    {
        _roomTriggers = new RoomTrigger[2];
    }
    void Start()
    {
        _roomData = GetComponent<RoomData>();
    }

    void OnRoomEntered(int roomID)
    {
        if(_roomData.GetRoomID() == roomID)
        {
            BeginRoom();
        }
    }

    void OnRoomExited(int roomID)
    {
        if (_roomData.GetRoomID() == roomID)
        {
            isActiveRoom = false;
            //set the room to be inactive and stuff
        }
    }

    void BeginRoom() //will be called once a player has entered a room.
    {
        if (_enemyWaves.Length == 0)
        {
            return; //dont bother closing the door
        }
        isActiveRoom = true;
        foreach (RoomTrigger trigger in _roomTriggers)
        {
            if (trigger.GetDoorTriggerType() == DoorTriggerType.EXIT) //complete room, disable the exit door.
            {
                trigger.SetDoorActive(true);
                break;
            }
        }
        StartCoroutine(CoroutineInitialiseWave());
    }

    public void AssignTrigger(RoomTrigger trigger)
    {
        if (_roomTriggers[0] == null) {
            _roomTriggers[0] = trigger;
            return;
        }
        if (_roomTriggers[1] != null)
        {
            Debug.LogError("ERROR: Too many triggers assigned.");
            return;
        }
        _roomTriggers[1] = trigger;
    }

    private void OnWaveComplete()
    {
        if (!isActiveRoom) return;

        _enemyWaves[_currentWaveID].SetActive(false);
        _currentWaveID++;
        if(_currentWaveID == _enemyWaves.Length) //if the final wave is complete
        {
            OnRoomComplete();
            return;
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
        Debug.Log("THIS WAVE HAS ENMIES THAT NUMBER " + _enemyCount);

    }

    void OnEnemyKilled()
    {
        if (!isActiveRoom) return;

        _enemyCount--;
        Debug.Log(_enemyCount);
        if (_enemyCount < 0) Debug.LogError("Negative Enemy count, something has gone wrong.");
        if (_enemyCount == 0)
        {
            Callbacks.CallEvent(CallbackEvent.WaveCompleted);
        }
    }

    void OnNewEnemyCreated()
    {
        if (!isActiveRoom) return;

        _enemyCount++;
    }

    void OnRoomComplete()
    {
        if (!isActiveRoom) return;

        foreach (RoomTrigger trigger in _roomTriggers)
        {
            if(trigger.GetDoorTriggerType() == DoorTriggerType.EXIT) //complete room, disable the exit door.
            {
                trigger.SetDoorActive(false);
                break;
            }
        }
        Callbacks.CallEvent(CallbackEvent.RoomCompleted);
    }
}
