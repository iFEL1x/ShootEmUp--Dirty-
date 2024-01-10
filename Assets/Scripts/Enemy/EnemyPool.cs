using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemySpawnPositions _enemySpawnPositions;
        [SerializeField] private GameObject _character;
        [SerializeField] private Transform _worldTransform;
        [Header("Pool")] 
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;

        private readonly Queue<GameObject> _enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(this._prefab, this._container);
                this._enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!this._enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(this._worldTransform);

            var spawnPosition = this._enemySpawnPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = this._enemySpawnPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this._character);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
        }
    }
}