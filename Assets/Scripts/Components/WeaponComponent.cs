using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        
        public Vector2 Position => this.firePoint.position;

        public Quaternion Rotation => this.firePoint.rotation;
    }
}