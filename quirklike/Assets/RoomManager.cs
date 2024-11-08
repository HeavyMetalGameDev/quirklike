using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyWaves;
    int _currentWaveID = 0;
    int _enemyCount = 0;
    float _waveTimeDelay = 1.0f;

    private void OnEnable()
    {
        Callbacks.EnemyKilled += OnEnemyKilled;
        Callbacks.EnemySpawned += OnNewEnemyCreated;
        Callbacks.WaveCompleted += OnWaveComplete;
    }

    private void OnDisable()
    {
        Callbacks.EnemyKilled -= OnEnemyKilled;
        Callbacks.EnemySpawned -= OnNewEnemyCreated;
        Callbacks.WaveCompleted -= OnWaveComplete;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnWaveComplete()
    {
        _enemyWaves[_currentWaveID].SetActive(false);
        StartCoroutine(CoroutineInitialiseWave());
    }

    IEnumerator CoroutineInitialiseWave()
    {
        yield return new WaitForSeconds(_waveTimeDelay);
        GameObject newWave = _enemyWaves[++_currentWaveID];
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
}
