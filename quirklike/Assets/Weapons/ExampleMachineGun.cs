using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExampleMachineGun : WeaponBase
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] float fireRate = 0.5f;
    float firePeriod = 0;
    float fireTimer = 0.0f;


    private void Awake()
    {
        RecalculateFirePeriod();
    }
    public void RecalculateFirePeriod()
    {
        if (fireRate == 0.0f) return; //just in case
        firePeriod = 1.0f / fireRate;
    }

    public override float GetDamage()
    {
        throw new System.NotImplementedException();
    }

    public override float GetFireRate()
    {
        throw new System.NotImplementedException();
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
            Debug.Log("BAM");
            fireTimer -= firePeriod;
            //fire tha weapon



            //do damage if we hit an enemy?

            RaycastHit hit;
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, 300.0f, GameRaycastLayers.defaultGunRaycastMask))
            {
                Vector3 hitPos = hit.point;

                if (hit.collider.CompareTag("Enemy"))
                {
                    var stats = hit.collider.GetComponent<EnemyStats>();
                    stats.DoDamage(damage);
                    //there will be most stuff here i.e events called, visuals etc.
                }
            }

            //Debug.Log(hitPos);
            //Vector3[] positions = {hitPos, transform.position};
            //CreateDebugLineRenderer(ref positions);
        }
    }


    //this is only for testing the weapons
    void CreateDebugLineRenderer(ref Vector3[] positions)
    {
        GameObject line = new GameObject();
        var renderer = line.AddComponent<LineRenderer>();
        renderer.SetPositions(positions);
    }

}
