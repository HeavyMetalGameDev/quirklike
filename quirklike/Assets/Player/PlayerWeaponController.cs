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
        weapon = null;
        //maybe move it to a different place / give it some velocity? / reenable rigidbody
    }
}

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] List<WeaponSlot> _currentWeaponSlots;
    int _numberOfAssignedWeapons = 0;
    private PlayerInputManager _playerInputManager;

    public event System.Action OnPlayerFireClicked;
    public event System.Action OnPlayerFireHeld;
    public event System.Action OnPlayerFireReleased;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputManager = PlayerInputManager.Instance;
        foreach (WeaponSlot weaponSlot in _currentWeaponSlots) //this is only here for testing.
        {
            WeaponBase weapon = weaponSlot.transform.GetComponentInChildren<WeaponBase>();
            if (weapon) AttachWeapon(weapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInputManager.PlayerFireDown()) //change these to new input system
        {
            OnPlayerFireClicked?.Invoke();
        }
        else if (_playerInputManager.PlayerFireHeld())
        {
            OnPlayerFireHeld?.Invoke();
        }
        else if (_playerInputManager.PlayerFireUp())
        {
            OnPlayerFireReleased?.Invoke();
        }
    }

    void AttachAlreadyHeldWeapon(ref WeaponBase weapon) //used when the weapon is already in the current weapons list, might be needed sometimes idk
    {
        Debug.Log(weapon.gameObject);
        OnPlayerFireClicked += weapon.OnInputClicked;
        OnPlayerFireHeld += weapon.OnInputHeld;
        OnPlayerFireReleased += weapon.OnInputReleased;
    }

    public void AttachWeapon(WeaponBase weapon ) //should only be called once we know the player wants to pickup/swap this weapon i.e after they choose what to swap.
    {
        WeaponSlot freeSlot = GetFreeWeaponSlot();
        if (freeSlot == null) return; //all slots are ful :(
        freeSlot.weapon = weapon;
        freeSlot.ReparentWeapon();
        weapon.PickUpWeapon();

        weapon._cameraTransform = Camera.main.transform;

        _numberOfAssignedWeapons++;

        OnPlayerFireClicked += weapon.OnInputClicked;
        OnPlayerFireHeld += weapon.OnInputHeld;
        OnPlayerFireReleased += weapon.OnInputReleased;
    }

    void DropWeapon(int weaponIndexID)
    {
        WeaponSlot droppedSlot = _currentWeaponSlots[weaponIndexID];
        WeaponBase droppedWeapon = droppedSlot.weapon;
        droppedSlot.DropWeaponFromSlot();
        droppedWeapon.DropWeapon();

        _numberOfAssignedWeapons--;
        OnPlayerFireClicked -= droppedWeapon.OnInputClicked;
        OnPlayerFireHeld -= droppedWeapon.OnInputHeld;
        OnPlayerFireReleased -= droppedWeapon.OnInputReleased;
    }

    WeaponSlot GetFreeWeaponSlot()
    {
        foreach(WeaponSlot slot in _currentWeaponSlots)
        {
            if (slot.weapon == null)
            {
                return slot;
            }
        }
        return null;
    }
}
