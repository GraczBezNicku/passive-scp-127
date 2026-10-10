using InventorySystem;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Features.Wrappers;
using UnityEngine;

using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace PassiveScp127;

public static class VectorExtensions
{
    public static Player[] GetPlayersWithinRange(this Vector3 origin, float range)
    {
        return [.. Player.ReadyList.Where(x => 
            x.IsAlive 
            && Vector3.Distance(x.Position, origin) <= range 
            && !Physics.Linecast(x.Camera.position, origin, ThrownProjectile.HitBlockerMask))];
    }
}