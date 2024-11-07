using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class AttackState : State
{
    private readonly Wanderer wanderer;
    private readonly NavMeshAgent agent;
    private readonly Animator Animator;
    private readonly AttackEventWatcher AEWatcher;
    private static readonly int Attacking = Animator.StringToHash("StartedAttack");
    private float AttackTime = 1.0f;
    private float cdcache = 1.0f;
    //to ensure no stutters happen if update is called before the anim starts
    private bool buffer = false;
    public AttackState(Wanderer w,  NavMeshAgent a, Animator anim, AttackEventWatcher ae, float cd)
    {
        this.wanderer   = w;
        this.agent      = a;
        this.Animator   = anim;
        this.AEWatcher  = ae;
        this.AttackTime = cd;
        cdcache = cd;
    }

    public void DoAttack()
    {
        Animator.SetTrigger(Attacking);
    }
    public void Tick()
    {
        if(AEWatcher.GetHasAttacked)
        {
            AttackTime -= Time.deltaTime;
        }
        if(AttackTime < 0f)
        {
            AEWatcher.SetCanAttackAgain();
            buffer = true;
        }
        
        if(!AEWatcher.GetHasAttacked && !AEWatcher.GetIsAttackOngoing && buffer)
        {
            AttackTime = cdcache;
            DoAttack();
            buffer = false;
        }
        
    }

    public void OnEnter()
    {
        AttackTime = 5.0f;
        agent.enabled = true;
        wanderer.StateName = "attack";
        Animator.SetTrigger(Attacking);
    }

    public void OnExit(){
        
    }
}
