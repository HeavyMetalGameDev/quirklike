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
    public Vector3[] PatrolPoints;
    
    private void Awake() 
    {
        StateMachine = new StateMachine();
        var NavAgent = GetComponent<NavMeshAgent>();

        var idle    = new IdleState(this);
        var patrol  = new PatrolState(this, NavAgent, PatrolPoints);

      
        StateMachine.AddTransition(idle, patrol, IsPatrolling);
        StateMachine.AddTransition(patrol, idle, NotPatrolling);

        bool IsPatrolling()   => isPatrolling; 
        bool NotPatrolling()  => !isPatrolling;

        StateMachine.SetState(idle);
        
    }

    private void Update() => StateMachine.Tick();


}
