using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CallbackData
{

}

public enum CallbackEvent
{
    NullEvent,
    TestEvent,
    TestIntEvent,
    EnemyKilled,
    EnemySpawned,
    SceneLoaded,
    RoomEntered,
    WaveCompleted,
    RoomCompleted,
    WeaponPickedUp,
    WeaponDropped,
    PlayerHurt,
    PlayerKilled,
    GameLost,
    AreaComplete
}


public static class Callbacks
{
    // these events may need their own parameters in the future
    public static event System.Action TestEvent;
    public static event System.Action EnemyKilled;
    public static event System.Action EnemySpawned;
    public static event System.Action SceneLoaded;
    public static event System.Action RoomEntered;
    public static event System.Action WaveCompleted;
    public static event System.Action RoomCompleted;
    public static event System.Action WeaponPickedUp;
    public static event System.Action WeaponDropped;
    public static event System.Action PlayerHurt;
    public static event System.Action PlayerKilled;
    public static event System.Action GameLost;
    public static event System.Action AreaComplete;


    public static void CallEvent(CallbackEvent callbackEvent, CallbackData data = null) //this can be called from anywhere, be careful
    {
        switch (callbackEvent)
        {
            case CallbackEvent.TestEvent:
                {
                    Debug.Log("TEST EVENT CALLED");
                    TestEvent?.Invoke();
                    break;
                }
            case CallbackEvent.EnemyKilled:
                {
                    Debug.Log("ENEMY HAS BEEN KILLED");
                    EnemyKilled?.Invoke();
                    break;
                }
            case CallbackEvent.EnemySpawned:
                {
                    Debug.Log("ENEMY SPAWNED");
                    EnemySpawned?.Invoke();
                    break;
                }
            case CallbackEvent.SceneLoaded:
                {
                    Debug.Log("SCENE LOADED");
                    SceneLoaded?.Invoke();
                    break;
                }
            case CallbackEvent.RoomEntered:
                {
                    Debug.Log("ROOM ENTERED");
                    RoomEntered?.Invoke();
                    break;
                }
            case CallbackEvent.WaveCompleted:
                {
                    Debug.Log("WAVE COMPLETED");
                    WaveCompleted?.Invoke();
                    break;
                }
            case CallbackEvent.RoomCompleted:
                {
                    Debug.Log("ROOM COMPLETED");
                    RoomCompleted?.Invoke();
                    break;
                }
            case CallbackEvent.WeaponPickedUp:
                {
                    Debug.Log("WEAPON PICKED UP");
                    WeaponPickedUp?.Invoke();
                    break;
                }
            case CallbackEvent.WeaponDropped:
                {
                    Debug.Log("WEAPON DROPPED");
                    WeaponDropped?.Invoke();
                    break;
                }
            case CallbackEvent.PlayerHurt:
                {
                    Debug.Log("PLAYER HURT");
                    PlayerHurt?.Invoke();
                    break;
                }
            case CallbackEvent.PlayerKilled:
                {
                    Debug.Log("PLAYER KILLED");
                    PlayerKilled?.Invoke();
                    break;
                }
            case CallbackEvent.GameLost:
                {
                    Debug.Log("GAME LOST");
                    GameLost?.Invoke();
                    break;
                }
            case CallbackEvent.AreaComplete:
                {
                    Debug.Log("AREA COMPLETE");
                    AreaComplete?.Invoke();
                    break;
                }
            default:
                {
                    Debug.LogError("EVENT NOT IMPLEMENTED");

                    break;
                }
        }
    }
}
