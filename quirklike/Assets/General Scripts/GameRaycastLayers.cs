using UnityEngine;

public static class GameRaycastLayers
{
    public static LayerMask defaultGunRaycastMask = ~LayerMask.GetMask("Player", "Weapon","PlayerDamageSource", "EnemyDamageSource","NeutralDamageSource", "Trigger", "IgnoreRaycast");

}
