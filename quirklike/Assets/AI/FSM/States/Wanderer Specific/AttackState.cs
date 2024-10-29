using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private readonly Wanderer wanderer;
    private readonly NavMeshAgent agent;

    public AttackState(Wanderer w,  NavMeshAgent a)
    {

        this.wanderer = w;
        this.agent = a;

    }

    public void Tick(){
        
    }

    public void OnEnter(){
        agent.enabled = true;
        Debug.Log("Attack state");
    }

    public void OnExit(){
        
    }
}
