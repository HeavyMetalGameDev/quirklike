using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot
{
    public WeaponBase weapon;
    public Transform transform;

    public void ReparentWeapon()
    {
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localScale = Vector3.one;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public void DropWeaponFromSlot()
    {
        weapon.transform.parent = null;
        //maybe move it to a different place / give it some velocity? / reenable rigidbody
    }
}

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] List<WeaponSlot> currentWeaponSlots;
    [SerializeField] List<WeaponBase> testWeapons;
    int numberOfAssignedWeapons = 0; 

    public event System.Action OnPlayerFireClicked;
    public event System.Action OnPlayerFireHeld;
    public event System.Action OnPlayerFireReleased;

    // Start is called before the first frame update
    void Start()
    {
        foreach(WeaponBase weapon in testWeapons) //this is only here for testing.
        {
            AttachWeapon(weapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //change these to new input system
        {
            OnPlayerFireClicked?.Invoke();
        }
        if (Input.GetMouseButton(0))
        {
            OnPlayerFireHeld?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnPlayerFireReleased?.Invoke();
        }
    }

    void AttachAlreadyHeldWeapon(WeaponBase weapon) //used when the weapon is already in the current weapons list, might be needed sometimes idk
    {
        Debug.Log(weapon.gameObject);
        OnPlayerFireClicked += weapon.OnInputClicked;
        OnPlayerFireHeld += weapon.OnInputHeld;
        OnPlayerFireReleased += weapon.OnInputReleased;
    }

    void AttachWeapon(WeaponBase weapon)
    {
        Debug.Log(weapon.gameObject);

        WeaponSlot freeSlot = GetFreeWeaponSlot();
        if (freeSlot == null) return; //all slots are ful :(
        freeSlot.weapon = weapon;
        freeSlot.ReparentWeapon();

        numberOfAssignedWeapons++;

        OnPlayerFireClicked += weapon.OnInputClicked;
        OnPlayerFireHeld += weapon.OnInputHeld;
        OnPlayerFireReleased += weapon.OnInputReleased;
    }

    void DropWeapon(int weaponIndexID)
    {
        WeaponSlot droppedSlot = currentWeaponSlots[weaponIndexID];
        WeaponBase droppedWeapon = droppedSlot.weapon;
        droppedSlot.weapon = null;

        numberOfAssignedWeapons--;
        OnPlayerFireClicked -= droppedWeapon.OnInputClicked;
        OnPlayerFireHeld -= droppedWeapon.OnInputHeld;
        OnPlayerFireReleased -= droppedWeapon.OnInputReleased;
    }

    WeaponSlot GetFreeWeaponSlot()
    {
        foreach(WeaponSlot slot in currentWeaponSlots)
        {
            if (slot.weapon == null)
            {
                return slot;
            }
        }
        return null;
    }
}
