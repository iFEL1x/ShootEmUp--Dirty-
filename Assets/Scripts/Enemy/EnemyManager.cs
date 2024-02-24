using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawnPositions _enemySpawnPositions;
        [SerializeField] private GameObject _character;
        [SerializeField] private EnemyPool _enemyPool;
        
        private readonly HashSet<GameObject> _activeEnemies = new();


        public void SpawnEnemy()
        {
            var enemy = this._enemyPool.Release();
            
            var spawnPosition = this._enemySpawnPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = this._enemySpawnPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this._character);
            if (this._activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmpty += this.OnDestroyed;
            } 
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= this.OnDestroyed;

                this._enemyPool.Release(enemy);
            }
        }
    }
}