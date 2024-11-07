using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CallbackData
{

}
public class CallbackFloat:CallbackData
{
    public float value;
    public CallbackFloat(float value)
    {
        this.value = value;
    }
}
public class CallbackTwoInts : CallbackData
{
    public int valueOne;
    public int valueTwo;
    public CallbackTwoInts(int valueOne, int valueTwo)
    {
        this.valueOne = valueOne;
        this.valueTwo = valueTwo;
    }
}

public class CallbackPlayerHitEnemyData : CallbackData
{
    public float damage;
    public bool isCritical;
    public GameObject enemyHit;
    public int playerID;

    public CallbackPlayerHitEnemyData(float damage, bool isCritical, GameObject enemyHit, int playerID)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        this.enemyHit = enemyHit;
        this.playerID = playerID;

    }
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
    PlayerHealed,
    PlayerKilled,
    PlayerHitEnemy,
    GameLost,
    AreaComplete,
    SwapWeaponSlots,
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
    public static event System.Action<float> PlayerHurt;
    public static event System.Action<float> PlayerHealed;
    public static event System.Action PlayerKilled;
    public static event System.Action<float,bool,GameObject,int> PlayerHitEnemy;
    public static event System.Action GameLost;
    public static event System.Action AreaComplete;
    public static event System.Action<int,int> SwapWeaponSlots;


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
                    CallbackFloat floatData = (CallbackFloat)data;
                    PlayerHurt?.Invoke(floatData.value);
                    break;
                }
            case CallbackEvent.PlayerHealed:
                {
                    Debug.Log("PLAYER HEALED");
                    CallbackFloat floatData = (CallbackFloat)data;
                    PlayerHealed?.Invoke(floatData.value);
                    break;
                }
            case CallbackEvent.PlayerKilled:
                {
                    Debug.Log("PLAYER KILLED");
                    PlayerKilled?.Invoke();
                    break;
                }
            case CallbackEvent.PlayerHitEnemy:
                {
                    Debug.Log("PLAYER HIT ENEMY");
                    CallbackPlayerHitEnemyData phed = (CallbackPlayerHitEnemyData)data;
                    PlayerHitEnemy?.Invoke(phed.damage,phed.isCritical,phed.enemyHit,phed.playerID);
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
            case CallbackEvent.SwapWeaponSlots:
                {
                    Debug.Log("TRY SWAP WEAPONS");
                    CallbackTwoInts slotIDs = (CallbackTwoInts)data;
                    SwapWeaponSlots?.Invoke(slotIDs.valueOne, slotIDs.valueTwo);
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
