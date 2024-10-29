using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class AttackState : State
{
    private readonly Wanderer wanderer;
    private readonly NavMeshAgent agent;
    private readonly Animator Animator;
    private static readonly int Attacking = Animator.StringToHash("StartedAttack");
    private Coroutine AttackCor;
    private float AttackTime = 30.0f;
    public AttackState(Wanderer w,  NavMeshAgent a, Animator anim)
    {
        this.wanderer = w;
        this.agent = a;
        this.Animator = anim;
    }

    public void startAttack(){

    }
    public void Tick(){
        AttackTime --;
        if(AttackTime < 0f){
            wanderer.attacked = true;
        }
    }

    public void OnEnter()
    {
        AttackTime = 30.0f;
        agent.enabled = true;
        wanderer.StateName = "attack";
        wanderer.CanAttack = false;
        Animator.SetTrigger(Attacking);
        wanderer.StartCoroutine("test");
    }

    public void OnExit(){
        
    }
}
