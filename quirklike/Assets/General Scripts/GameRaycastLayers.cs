using UnityEngine;

public static class GameRaycastLayers
{
    public static LayerMask defaultGunRaycastMask = ~LayerMask.GetMask("Player", "InHandWeapon","PlayerDamageSource", "EnemyDamageSource","NeutralDamageSource", "Trigger", "IgnoreRaycast");

}
