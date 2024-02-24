using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached { get; private set; }

        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private float _moveThreshold = 0.25f;
        
        private Vector2 _destination;

        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this.IsReached = false;
        }

        private void FixedUpdate()
        {
            if (this.IsReached)
            {
                return;
            }
            
            var vector = this._destination - (Vector2) this.transform.position;
            if (vector.sqrMagnitude <= this._moveThreshold * this._moveThreshold)
            {
                this.IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this.moveComponent.Move(direction);
        }
    }
}