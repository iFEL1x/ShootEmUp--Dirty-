using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private LevelBackgroundParams levelBackgroundParams;
        
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;
        
        private void Awake()
        {
            this._startPositionY = this.levelBackgroundParams.StartPositionY;
            this._endPositionY = this.levelBackgroundParams.EndPositionY;
            this._movingSpeedY = this.levelBackgroundParams.MovingSpeedY;
            this._myTransform = this.transform;
            
            var position = this._myTransform.position;
            this._positionX = position.x;
            this._positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (this._myTransform.position.y <= this._endPositionY)
            {
                this._myTransform.position = new Vector3(
                    this._positionX,
                    this._startPositionY,
                    this._positionZ
                );
            }

            this._myTransform.position -= new Vector3(
                this._positionX,
                this._movingSpeedY * Time.fixedDeltaTime,
                this._positionZ
            );
        }
    }
}