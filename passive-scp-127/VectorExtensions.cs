using InventorySystem;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Features.Wrappers;
using UnityEngine;

using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace passive_scp_127;

public static class VectorExtensions
{
    private static LayerMask? grenadeDetectionMask;
    public static LayerMask GrenadeDetectionMask
    {
        get
        {
            if (grenadeDetectionMask.HasValue)
                return grenadeDetectionMask.Value;

            if (!InventoryItemLoader.TryGetItem<ThrowableItem>(ItemType.GrenadeHE, out var throwableItem))
            {
                return default;
            }

            ExplosionGrenade explosionGrenade = (throwableItem.Projectile as ExplosionGrenade)!;

            if (explosionGrenade == null)
            {
                return default;
            }

            grenadeDetectionMask = explosionGrenade.DetectionMask;
            return grenadeDetectionMask.Value;
        }
    }

    public static Player[] GetPlayersWithinRange(this Vector3 origin, float range)
    {
        Collider[] result = Physics.OverlapSphere(origin, range, GrenadeDetectionMask);

        List<Player> foundPlayers = new List<Player>();
        HashSet<uint> handledIds = new HashSet<uint>();

        foreach (Collider col in result)
        {
            if (col.TryGetComponent<IDestructible>(out var dest))
            {
                if (!Physics.Linecast(dest.CenterOfMass, origin, ThrownProjectile.HitBlockerMask) && !handledIds.Contains(dest.NetworkId))
                {
                    if (dest is HitboxIdentity hitbox)
                    {
                        Player target = Player.Get(hitbox.TargetHub);

                        if (!foundPlayers.Contains(target))
                            foundPlayers.Add(target);
                    }
                    handledIds.Add(dest.NetworkId);
                }
            }
        }

        return foundPlayers.ToArray();
    }
}