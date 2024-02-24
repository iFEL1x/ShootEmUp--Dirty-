using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [field: SerializeField] internal PhysicsLayer PhysicsLayer { get; private set; }
        [field: SerializeField] internal int Damage { get; private set; }
        [field: SerializeField] internal float Speed { get; private set; }
        [field: SerializeField] internal bool IsPlayer { get; private set; }
        [field: SerializeField] internal Color Color { get; private set; }
    }
}