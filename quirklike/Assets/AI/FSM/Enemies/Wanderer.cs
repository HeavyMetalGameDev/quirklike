using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
    private StateMachine StateMachine;
    public bool HasTarget = false;
    public bool isPatrolling = false;
    public Transform debug;
    public Vector3[] PatrolPoints;
    
    private void Awake() 
    {
        StateMachine = new StateMachine();
        var NavAgent = GetComponent<NavMeshAgent>();

        var idle    = new IdleState(this);
        var run     = new WanderState(this, NavAgent);
        var patrol  = new PatrolState(this, NavAgent, PatrolPoints);

        StateMachine.AddTransition(idle, run, HasValidTarget);
        StateMachine.AddTransition(run, idle, NoValidTarget);
        StateMachine.AddTransition(idle, patrol, IsPatrolling);
        StateMachine.AddTransition(patrol, idle, NotPatrolling);


        bool HasValidTarget() => HasTarget;
        bool NoValidTarget()  => !HasTarget;  
        bool IsPatrolling()   => isPatrolling; 
        bool NotPatrolling() => !isPatrolling;

        StateMachine.SetState(idle);
        
    }

    private void Update() => StateMachine.Tick();
}
