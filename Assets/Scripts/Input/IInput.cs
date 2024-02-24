using System;

namespace ShootEmUp
{
    public interface IInput
    {
        public event Action OnFire;
        public event Action<float> OnMove; 
    }
}
