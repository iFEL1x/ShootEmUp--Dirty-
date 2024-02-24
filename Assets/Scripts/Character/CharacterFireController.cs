using UnityEngine;

namespace ShootEmUp
{
    public class CharacterFireController : MonoBehaviour
    {
        [SerializeField] private float _coolDown = 1f;
        [SerializeField] private WeaponComponent _weaponComponent; 
        [SerializeField] private BulletSystem _bulletsController;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private KeyboardInput _input;
        
        private float _currentTimer;
        private void OnEnable()
        {
            this._input.OnFire += Fire;
        }

        private void OnDisable()
        {
            this._input.OnFire -= Fire;
        }

        private void FixedUpdate()
        {
            this._currentTimer += Time.deltaTime;
        }

        private void Fire()
        {
            if(this._currentTimer >= this._coolDown)
            {
                var position = this._weaponComponent.Position;
                var direction = this._weaponComponent.Rotation * Vector2.up;

                this._bulletsController.SpawnBullet(position, direction, this._bulletConfig);
                this._currentTimer = 0f;
            }
        }
    }
}
