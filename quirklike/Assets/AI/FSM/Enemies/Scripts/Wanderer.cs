using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
    private StateMachine StateMachine;
    [Header("State Variables")]
    [Tooltip("Idle to Patrolling")]
    public bool isPatrolling = false;

    [Tooltip("Points to patrol between. Make sure the Y values are the same/higher than the agent height")]
    public Vector3[] PatrolPoints;

    [Header("Debug")]
    public GameObject Player;
    public bool isReals;

    public string StateName;
    
    
    private void Awake() 
    {
        StateMachine = new StateMachine();
        var NavAgent = GetComponent<NavMeshAgent>();

        var idle    = new IdleState(this);
        var patrol  = new PatrolState(this, NavAgent, PatrolPoints);
        var chase   = new ChaseState(this);
      
        StateMachine.AddTransition(idle, patrol, IsPatrolling);
        StateMachine.AddTransition(patrol, idle, NotPatrolling);

        StateMachine.AddAnyTransition(chase, ()=> isReals);
        StateMachine.AddTransition(chase, idle, ()=> !isReals);

        bool IsPatrolling()   => isPatrolling; 
        bool NotPatrolling()  => !isPatrolling;
        bool isReal() => isReals;

        StateMachine.SetState(idle);


       //Debug UI 
       var canvas = transform.Find("Canvas").gameObject;
       canvas.SetActive(true);
    }

    private void Update() => StateMachine.Tick();

    public string getStateName(){
        return StateName;
    }

}
