using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private BulletPool _pool;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();
        
        private void FixedUpdate()
        {
            ControlBoundsPositionBullets();
        }

        private void ControlBoundsPositionBullets()
        {
            this._cache.Clear();
            this._cache.AddRange(this._activeBullets);

            for (int i = 0, count = this._cache.Count; i < count; i++)
            {
                var bullet = this._cache[i];
                if (!this._levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }
        
        public void SpawnBullet(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            Bullet bullet = this._pool.Get();
            bullet.SetArgs(position, direction, bulletConfig);
            
            if (this._activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.RemoveBullet;
            }
        }
        
        private void RemoveBullet(Bullet bullet)
        {
            if (this._activeBullets.Remove(bullet))
            {
                _pool.Release(bullet);
                bullet.OnCollisionEntered -= this.RemoveBullet;
            }
        }
    }
}
