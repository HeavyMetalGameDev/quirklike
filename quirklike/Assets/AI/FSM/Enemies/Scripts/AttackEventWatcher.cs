using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventWatcher : MonoBehaviour
{
    [SerializeField] private bool IsAttackOngoing;
    [SerializeField] private bool HasAttacked;
    // Start is called before the first frame update
    void Start()
    {
        IsAttackOngoing = false;
    }

    public void StartAttacking()
    {
        IsAttackOngoing = true;
        HasAttacked     = true;
    }
    public void EndAttacking(){
        IsAttackOngoing = false;
    }

    public void SetCanAttackAgain()
    {
        HasAttacked = false;
    }

    public bool GetHasAttacked{
        get { return HasAttacked; }
    }
    public bool GetIsAttackOngoing{
        get{ return IsAttackOngoing ; }
    }
}
