using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private readonly Wanderer wanderer;
    private readonly AttackRadius attackRadius;
    public AttackState(Wanderer w, AttackRadius a){
        this.wanderer = w;
        this.attackRadius = a;
    }

    public void Tick(){

    }

    public void OnEnter(){
        attackRadius.attacking = true;
    }

    public void OnExit(){
        attackRadius.attacking = false;
    }
}
