using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public Transform _cameraTransform;
    public abstract void OnInputClicked(); //probably useful for sniper like weapons or semi auto

    public abstract void OnInputHeld(); //probably useful for fully auto weapons

    public abstract void OnInputReleased(); //probably useful for charged weapons


    public abstract float GetDamage();
    public abstract float GetFireRate();

    //maybe more get methods?
}
