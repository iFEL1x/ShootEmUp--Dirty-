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
            
            this.transform.position = args.Position;
            this._rigidbody2D.velocity = args.Velocity;
            this._spriteRenderer.color = args.Color;
            this.gameObject.layer = args.PhysicsLayer;
            this._damage = args.Damage;
            this._isPlayer = args.IsPlayer;
        }
    }
}