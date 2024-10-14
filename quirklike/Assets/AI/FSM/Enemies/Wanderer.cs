using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    private StateMachine StateMachine;
    public int time = 0; 
    public bool us = false;
    
    private void Awake() {
        var height = this.transform.position.y;

        StateMachine = new StateMachine();

        var inst = new BirthTest(this);
        var alive = new AliveTest(this);
        StateMachine.AddTransition(alive, inst, isLIving);

        bool isLIving() =>us;
        StateMachine.SetState(alive);
        
    }

    private void Update() 
    {
        time ++;
        StateMachine.Tick();
    }
}
