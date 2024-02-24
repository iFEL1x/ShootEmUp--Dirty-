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

        private readonly Queue<Bullet> _bulletPool = new();
        
        private void Awake()
        {
            CreateBullets();
        }

        private void CreateBullets()
        {
            for (var i = 0; i < this._initialCount; i++)
            {
                var bullet = Instantiate(this._prefab, this._container);
                bullet.gameObject.SetActive(false);
                this._bulletPool.Enqueue(bullet);
            }
        }
        
        public Bullet Get()
        {   
            if (this._bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this._worldTransform);
            }
            else
            {
                bullet = Instantiate(this._prefab, this._worldTransform);
            }

            bullet.gameObject.SetActive(true);
            return bullet;
        }

        public void Release(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(this._container);
            this._bulletPool.Enqueue(bullet);
        }
    }
}