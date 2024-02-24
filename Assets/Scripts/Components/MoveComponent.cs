using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.0f;
        
        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            this._rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 vector)
        {
            var nextPosition = this._rigidbody2D.position + vector * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}