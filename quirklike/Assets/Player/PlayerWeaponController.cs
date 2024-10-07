using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] WeaponBase testWeapon;

    List<WeaponBase> currentWeapons;

    public event System.Action OnPlayerFireClicked;
    public event System.Action OnPlayerFireHeld;
    public event System.Action OnPlayerFireReleased;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapons = new List<WeaponBase>();
        AttachWeapon(testWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

    void AttachWeapon(WeaponBase weapon)
    {
        Debug.Log(weapon.gameObject);
        currentWeapons.Add(weapon);

        OnPlayerFireClicked += weapon.OnInputClicked;
        OnPlayerFireHeld += weapon.OnInputHeld;
        OnPlayerFireReleased += weapon.OnInputReleased;
    }

    void DropWeapon(int weaponIndexID)
    {
        WeaponBase droppedWeapon = currentWeapons[weaponIndexID];
        currentWeapons.Remove(droppedWeapon);

        OnPlayerFireClicked -= droppedWeapon.OnInputClicked;
        OnPlayerFireHeld -= droppedWeapon.OnInputHeld;
        OnPlayerFireReleased -= droppedWeapon.OnInputReleased;
    }
}
