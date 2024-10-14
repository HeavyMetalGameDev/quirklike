using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
    private StateMachine StateMachine;
    public bool HasTarget = false;
    public Transform debug;
    
    private void Awake() {
        StateMachine = new StateMachine();
        var NavAgent = GetComponent<NavMeshAgent>();

        var idle = new IdleState(this);
        var run = new WanderState(this, NavAgent);



        StateMachine.AddTransition(idle, run, HasValidTarget);
        StateMachine.AddTransition(run, idle, NoValidTarget);

        bool HasValidTarget() =>HasTarget;
        bool NoValidTarget() => !HasTarget;  

        StateMachine.SetState(idle);
        
    }

    private void Update() 
    {
        StateMachine.Tick();
    }
}
