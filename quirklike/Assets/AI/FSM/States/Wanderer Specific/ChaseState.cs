using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private readonly Wanderer wanderer;
    private NavMeshAgent NavAgent;

    private const float CHASE_SPEED = 6f;
    private PlayerDetector Detector;
    private readonly Animator Animator;
    private static readonly int IsChasing = Animator.StringToHash("IsChasing");

    public ChaseState(Wanderer w,  NavMeshAgent a, Animator anim)
    {
        wanderer = w;
        NavAgent = a;
        Animator = anim;
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
        Detector = wanderer.GetDetector();
        Animator.SetBool(IsChasing, true);
        
    }
    public void OnExit()
    {
        NavAgent.enabled = false;
        Animator.SetBool(IsChasing, false);
    }
}
