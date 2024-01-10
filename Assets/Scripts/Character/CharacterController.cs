using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        public bool FireRequired { get; set; }

        [SerializeField] private GameObject _character; 
        [SerializeField] private WeaponComponent _weaponComponent; 
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BulletsController _bulletsController;
        [SerializeField] private BulletConfig _bulletConfig;
        
        private void OnEnable()
        {
            this._character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            this._character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
        }
        
        private void FixedUpdate()
        {
            if (this.FireRequired)
            {
                this.OnFire();
                this.FireRequired = false;
            }
        }
        
        private void OnCharacterDeath(GameObject _)
        {
            this._gameManager.FinishGame();
        }

        private void OnFire()
        {
            var position = _weaponComponent.Position;
            var direction = _weaponComponent.Rotation * Vector2.up;
            
            _bulletsController.SetupBullet(position, direction, _bulletConfig);
        }
    }
}