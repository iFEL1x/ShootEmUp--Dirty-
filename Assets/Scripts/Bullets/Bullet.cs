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

            if (_isPlayer == team.IsPlayer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(_damage);
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
    }
}