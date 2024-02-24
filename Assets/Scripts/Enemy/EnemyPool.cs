using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private Transform _worldTransform;
        [Header("Pool")] 
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private int poolSize = 7;

        private readonly Queue<GameObject> _enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.poolSize; i++)
            {
                var enemy = Instantiate(this._prefab, this._container);
                    this._enemyPool.Enqueue(enemy);
            }
            
        }

        /*public bool TryGetEnemy(out GameObject enemy)
        {
            if (this._enemyPool.TryDequeue(out enemy))
            {
                return true;
            }
            
            enemy = Instantiate(this._prefab, this._container);
            return enemy;
        }*/

        public GameObject Release()
        {
            if (!this._enemyPool.TryDequeue(out var enemy))
            {
                enemy = Instantiate(this._prefab, this._container);
            }

            enemy.transform.SetParent(this._worldTransform);
            enemy.GetComponent<EnemyAttackAgent>().SetBulletSystem(this._bulletSystem);
            
            return enemy;
        }

        public void Release(GameObject enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
        }
    }
}