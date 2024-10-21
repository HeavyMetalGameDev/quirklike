using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is the most basic weapon class, which has very simple functionality.
//there are other weapon classes that derive from this which may be more appropriate, unless you want to make a weapon from scratch.
public abstract class WeaponBase : MonoBehaviour
{
    public Transform _cameraTransform; 
    [SerializeField] protected Transform _firePoint; //this is the position where "bullets" should come out (if appropriate)
    public abstract void OnInputClicked(); //probably useful for sniper like weapons or semi auto

    public abstract void OnInputHeld(); //probably useful for fully auto weapons

    public abstract void OnInputReleased(); //probably useful for charged weapons


    public abstract float GetDamage();
    public abstract float GetFireRate();

    //maybe more get methods?
}
