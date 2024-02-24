using System;
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyCooldownSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private float _spawnCooldown = 1f;
        
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(this._spawnCooldown);
                
                this._enemyManager.SpawnEnemy();
            }
        }
    }
}
