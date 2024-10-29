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
    [SerializeField] GameObject _detectionRadiusObject;

    [Tooltip("Points to patrol between. Make sure the Y values are the same/higher than the agent height")]
    public Vector3[] PatrolPoints;

    [Header("Debug")]
    public string StateName;
    public bool Debug;
    public bool range;
    
    private void Awake() 
    {
        StateMachine = new StateMachine();
        var NavAgent = GetComponent<NavMeshAgent>();

        var PlayerDetector = _detectionRadiusObject.AddComponent<PlayerDetector>();
        var EStats         = gameObject.AddComponent<EStats>();

        var idle    = new IdleState(this, NavAgent);
        var patrol  = new PatrolState(this, NavAgent, PatrolPoints);
        var chase   = new ChaseState(this, NavAgent);
        var attack  = new AttackState(this, NavAgent);
      
        StateMachine.AddTransition(idle, patrol, IsPatrolling);
        StateMachine.AddTransition(patrol, idle, NotPatrolling);

        // StateMachine.AddAnyTransition(chase, ()=> PlayerDetector.PlayerInRange);
        // StateMachine.AddTransition(chase, idle, ()=> PlayerDetector.PlayerInRange == false);

        StateMachine.AddAnyTransition(attack, ()=> PlayerDetector.PlayerInMRange);
        StateMachine.AddTransition(attack, idle, ()=> PlayerDetector.PlayerInMRange == false);    

        bool IsPatrolling()   => isPatrolling; 
        bool NotPatrolling()  => !isPatrolling;

        StateMachine.SetState(idle);

       //Debug UI 
       if(Debug){
        var canvas = transform.Find("Canvas").gameObject;
        canvas.SetActive(true);
       }
       
    }

    private void Update() => StateMachine.Tick();

    public string getStateName(){
        return StateName;
    }

    public PlayerDetector GetDetector()
    {
        return _detectionRadiusObject.GetComponent<PlayerDetector>();
    }

}
