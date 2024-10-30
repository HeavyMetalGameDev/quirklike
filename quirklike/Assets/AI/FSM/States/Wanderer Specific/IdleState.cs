using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    private readonly Wanderer wanderer;
    private readonly NavMeshAgent NavAgent;
    private readonly Animator Animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private int IDLE_TIME = 1000;
    
    public IdleState(Wanderer w, NavMeshAgent a, Animator anim){
        this.wanderer = w;
        this.NavAgent = a;
        this.Animator = anim;
    }

    public void Tick()
    { 
        IDLE_TIME --;
        if(IDLE_TIME < 0){
            wanderer.isPatrolling = true;
        }
    }

    public void OnEnter()
    {
        IDLE_TIME = 1000;
        wanderer.StateName = "idle";
        NavAgent.enabled = true;
        Animator.SetBool(IsMoving, false);
    }

    public void OnExit(){
        // Debug.Log("birth Exit");
        
    }
}
