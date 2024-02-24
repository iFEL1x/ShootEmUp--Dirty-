using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardInput : MonoBehaviour, IInput
    {
        public event Action OnFire;
        public event Action<float> OnMove; 
        public float HorizontalMove { get; private set; }
        
        private void Update()
        {
            if(Input.GetKey(KeyCode.Space))
                OnFire.Invoke();;

            HorizontalMove = Input.GetAxisRaw("Horizontal");
            OnMove?.Invoke(HorizontalMove);
        }
    }
}
