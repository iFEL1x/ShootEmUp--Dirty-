using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private bool _isPlayer;
        private int _damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (this._isPlayer == team.IsPlayer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(this._damage);
            }
            
            this.OnCollisionEntered?.Invoke(this);
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            this._rigidbody2D.velocity = velocity;
        }

        public void SetColor(Color color)
        {
            this._spriteRenderer.color = color;
        }
        
        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        public void SetDamage(int damage)
        {
            this._damage = damage;
        }
        
        public void SetPlayerTeam(bool isPlayer)
        {
            this._isPlayer = isPlayer;
        }

        public void SetArgs(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            BulletArgs args = new()
            {
                Position = position,
                Velocity = direction * bulletConfig.Speed,
                Color = bulletConfig.Color,
                PhysicsLayer = (int) bulletConfig.PhysicsLayer,
                Damage = bulletConfig.Damage,
                IsPlayer = bulletConfig.IsPlayer
            };
            
            SetPosition(args.Position); 
            SetVelocity(args.Velocity);
            SetColor(args.Color);
            SetPhysicsLayer(args.PhysicsLayer);
            SetDamage(args.Damage);
            SetPlayerTeam(args.IsPlayer);
        }
    }
}