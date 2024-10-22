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
}


public static class Callbacks
{
    public static event System.Action TestEvent;
    public static event System.Action EnemyKilled;


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
        }
    }
}
