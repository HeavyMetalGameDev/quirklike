using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AutoProjectileWeapon : WeaponBase
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] float baseFireRate = 0.5f; //this should sync up to the animation
    [SerializeField] float fireRateMultiplier = 1.0f; //this should be used to multiply the animation speed
    [SerializeField] AudioSource _gunAudioSource;
    [SerializeField] AudioClip _gunFireClip;
    [SerializeField] ObjectPool _projectilePool;

    Vector3 _debugLastHiscanPosition = Vector3.zero;

    float firePeriod = 0;
    float fireTimer = 0.0f;
    float trueFireRate = 0.5f;



    private void Awake()
    {
        RecalculateTrueFireRate();
        RecalculateFirePeriod();
        UpdateAnimationSpeed();
    }

    private void UpdateAnimationSpeed()
    {
        _animator.speed = fireRateMultiplier;
    }
    public void RecalculateTrueFireRate()
    {

        if (baseFireRate == 0.0f) return; //just in case
        trueFireRate = baseFireRate * fireRateMultiplier;
    }
    public void RecalculateFirePeriod()
    {

        if (trueFireRate == 0.0f) return; //just in case
        firePeriod = 1.0f / trueFireRate;
    }

    public override float GetDamage()
    {
        return damage;
    }

    public override float GetFireRate()
    {
        return trueFireRate;
    }

    public override void OnInputClicked()
    {
        TryFireWeapon();
    }

    public override void OnInputHeld()
    {
        TryFireWeapon();
    }

    public override void OnInputReleased()
    {
        //nothing for this weapon
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer < firePeriod)
        {
            fireTimer += Time.deltaTime;
        }
    }

    void TryFireWeapon()
    {
        if (fireTimer >= firePeriod)
        {
            fireTimer -= firePeriod;
            _animator.SetTrigger("BasicRecoil"); //this should be used on every gun to show recoil
            _gunAudioSource.PlayOneShot(_gunFireClip); //maybe should be more customisable



            //do damage if we hit an enemy?

            RaycastHit hit;
            Vector3 hitPos;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, 300.0f, GameRaycastLayers.defaultGunRaycastMask))
            {
                hitPos = hit.point;
            }
            else
            {
                hitPos = _cameraTransform.position + _cameraTransform.forward * 300.0f; //far in the distance
            }

            GameObject projectile = _projectilePool.GetFreeItem();
            projectile.transform.position = _firePoint.position;
            BasicProjectile projectileScript = projectile.GetComponent<BasicProjectile>();
            projectileScript.SetAlignment(DamageAlignment.PLAYER_SOURCE);
            projectileScript.SetDamage(damage);
            projectileScript.SetTargetPosition(hitPos);

        }
    }


    //this is only for testing the weapons
    void CreateDebugLineRenderer(ref Vector3[] positions)
    {
        GameObject line = new GameObject();
        var renderer = line.AddComponent<LineRenderer>();
        renderer.widthMultiplier = 0.1f;
        renderer.SetPositions(positions);
    }

}
