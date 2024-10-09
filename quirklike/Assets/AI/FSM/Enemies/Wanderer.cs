using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    private StateMachine StateMachine;
    public bool living = false; 
    
    private void Awake() {
        var height = this.transform.position.y;

        StateMachine = new StateMachine();

        var inst = new BirthTest(this);
        var alive = new AliveTest(this);

        StateMachine.AddTransition(inst, alive, Living());

        Func<bool> Living () => () =>  true;
        StateMachine.SetState(inst);
    }

    private void Update() 
    {
        StateMachine.Tick();
    }
}
