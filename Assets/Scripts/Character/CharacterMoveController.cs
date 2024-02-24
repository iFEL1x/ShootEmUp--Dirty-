using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveController : MonoBehaviour
    {
        [SerializeField] private KeyboardInput _input;
        [SerializeField] private MoveComponent _moveComponent;

        private void FixedUpdate()
        {
            this._moveComponent.Move(new Vector2(this._input.HorizontalMove, 0) * Time.fixedDeltaTime);
        }
    }
}
