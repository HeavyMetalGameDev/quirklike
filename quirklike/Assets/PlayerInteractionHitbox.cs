using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHitbox : MonoBehaviour
{
    [SerializeField] PlayerWeaponController _weaponController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            //show the info about the weapon and allow the player to swap the weapon
        }
    }
}
