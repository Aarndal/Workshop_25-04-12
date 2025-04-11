using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Scriptable Objects/Projectile")]
public class SOProjectile : ScriptableObject
{
    [SerializeField]
    public bool DestroyOnContact = true;

    [Tooltip("In Seconds"), Range(1.0f, 60.0f)]
    public float MaxLifeTime = 10.0f;

    [Range(0.1f, 1000.0f)]
    public float Velocity = 1.0f;

    [Min(0.001f)]
    public float Mass = 0.01f;
}
