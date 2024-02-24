using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private BulletSystem _bulletsSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private float _countdown;

        private GameObject _target;
        private float _currentTime;

        public void SetTarget(GameObject target)
        {
            this._target = target;
        }

        public void Reset()
        {
            this._currentTime = this._countdown;
        }

        private void FixedUpdate()
        {
            if (!this._moveAgent.IsReached)
            {
                return;
            }
            
            if (!this._target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0)
            {
                this.Fire();
                this._currentTime += this._countdown;
            }
        }

        private void Fire()
        {
            var startPosition = this._weaponComponent.Position;
            var vector = (Vector2) this._target.transform.position - startPosition;
            var direction = vector.normalized;
            
            _bulletsSystem.SpawnBullet(startPosition, direction, this._bulletConfig);
        }

        public void SetBulletSystem(BulletSystem system)
        {
            this._bulletsSystem = system;
        }
    }
}