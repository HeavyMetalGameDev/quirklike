using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private readonly Wanderer wanderer;
    private NavMeshAgent NavAgent;

    private const float CHASE_SPEED = 6f;
    private PlayerDetector Detector;
    public ChaseState(Wanderer w,  NavMeshAgent a)
    {
        wanderer = w;
        NavAgent = a;
    }

    public void Tick()
    {
        if(Detector != null){
            NavAgent.SetDestination(Detector.GetPlayerPosition());
        }
    }
    public void OnEnter()
    {
        NavAgent.enabled = true;
        NavAgent.speed = CHASE_SPEED;
        wanderer.StateName = "chase";
        Detector = wanderer.GetComponent<PlayerDetector>();
        
    }
    public void OnExit()
    {
        NavAgent.enabled = false;
    }
}
