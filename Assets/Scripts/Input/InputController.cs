using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputController : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private CharacterController _characterController;

        private float _horizontalDirection;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterController.FireRequired = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                this._horizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this._horizontalDirection = 1;
            }
            else
            {
                this._horizontalDirection = 0;
            }
        }
        
        private void FixedUpdate()
        {
            _moveComponent.Move(new Vector2(this._horizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}