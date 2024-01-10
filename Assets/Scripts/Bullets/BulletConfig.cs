using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] internal PhysicsLayer PhysicsLayer;
        [SerializeField] internal Color Color;
        [SerializeField] internal int Damage;
        [SerializeField] internal float Speed;
        [SerializeField] internal bool IsPlayer;
    }
}