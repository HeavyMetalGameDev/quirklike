using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : WeaponBase
{
    public override void OnInputClicked()
    {
        Debug.Log("TEST WEAPON CLICKED!!!");
    }

    public override void OnInputHeld()
    {
        Debug.Log("TEST WEAPON HELD!!!");

    }

    public override void OnInputReleased()
    {
        Debug.Log("TEST WEAPON RELEASED!!!");

    }
}
