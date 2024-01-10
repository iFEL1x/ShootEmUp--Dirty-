using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletsController : MonoBehaviour
    {
        [SerializeField] private BulletPool _bulletPool;
        
        public void SetupBullet(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            _bulletPool.SpawnBullet(new BulletPool.Args
            {
                Position = position,
                Velocity = direction * bulletConfig.Speed,
                Color = bulletConfig.Color,
                PhysicsLayer = (int) bulletConfig.PhysicsLayer,
                Damage = bulletConfig.Damage,
                IsPlayer = bulletConfig.IsPlayer
            });
        }
    }
}
