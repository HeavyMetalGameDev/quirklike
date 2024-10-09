using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    private StateMachine StateMachine;
    public int time = 0; 
    
    private void Awake() {
        var height = this.transform.position.y;

        StateMachine = new StateMachine();

        var inst = new BirthTest(this);
        var alive = new AliveTest(this);

        StateMachine.AddTransition(inst, alive, isLIving());

        Func<bool> isLIving() => () => time >0;
        StateMachine.SetState(alive);
    }

    private void Update() 
    {
        StateMachine.Tick();
    }
}
