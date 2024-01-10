using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();
        
        private void Awake()
        {
            CreateBullets();
        }
        
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

        private void CreateBullets()
        {
            for (var i = 0; i < this._initialCount; i++)
            {
                var bullet = Instantiate(this._prefab, this._container);
                this._bulletPool.Enqueue(bullet);
            }
        }
        
        public void SpawnBullet(Args args)
        {
            if (this._bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this._worldTransform);
            }
            else
            {
                bullet = Instantiate(this._prefab, this._worldTransform);
            }

            bullet.SetPosition(args.Position); 
            bullet.SetVelocity(args.Velocity);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.SetDamage(args.Damage);
            bullet.SetPlayerTeam(args.IsPlayer);
            
            if (this._activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.RemoveBullet;
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (this._activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.RemoveBullet;
                bullet.transform.SetParent(this._container);
                this._bulletPool.Enqueue(bullet);
            }
        }
        
        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}