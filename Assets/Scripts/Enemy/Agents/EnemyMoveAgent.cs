using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached { get; private set; }

        [SerializeField] private MoveComponent moveComponent;

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
            if (vector.magnitude <= 0.25f)
            {
                this.IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this.moveComponent.Move(direction);
        }
    }
}