using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PatrolState : State
{
    private readonly Wanderer wanderer;
    private readonly NavMeshAgent NavAgent;

    private readonly Vector3[] PatrolPoints;
    int CurPoint      = 0;
    bool TargetSwitch = false;

    //private readonly animator etc etc

    public PatrolState(Wanderer w, NavMeshAgent a, Vector3[] patrolpoints)
    {
        wanderer = w;
        NavAgent = a;
        PatrolPoints = patrolpoints;
    }

    public void Tick()
    {
        if(!NavAgent.pathPending)
        {
            if(NavAgent.remainingDistance < NavAgent.stoppingDistance)
            {
                if(!NavAgent.hasPath || NavAgent.velocity.sqrMagnitude == 0f)
                {
                    if(!TargetSwitch)
                    {
                        Debug.Log("switching targets!");
                        SwitchTargets();
                    } 
                }
                
            }
        }
        
    }

    void SwitchTargets()
    {
        TargetSwitch = true;
        CurPoint += 1;
        if(CurPoint >= PatrolPoints.Length)
        {
            CurPoint =0;
        }
        NavAgent.SetDestination(PatrolPoints[CurPoint]);
        TargetSwitch = false;
    }

    public void OnEnter()
    {
        NavAgent.enabled = true;
        NavAgent.SetDestination(PatrolPoints[0]);
    }

    public void OnExit()
    {
        NavAgent.enabled = false;
        TargetSwitch     = false;
        CurPoint = 0;
    }
}
