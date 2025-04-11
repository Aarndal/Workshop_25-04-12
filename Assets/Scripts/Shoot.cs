using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private int _selectedProjectile = 0;
    [SerializeField]
    private Projectile[] _loadedProjectiles;

    InstantiateParameters parameters;

InputAction _attack;

    private void Awake()
    {
        _attack = InputSystem.actions.FindAction("Attack");

        parameters.parent = transform;
        parameters.worldSpace = false;
    }

    private void OnEnable()
    {
        _attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        _attack.performed -= OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        ShootProjectile();
    }

    private void ShootProjectile()
    {
        Projectile projectile = Instantiate<Projectile>(_loadedProjectiles[_selectedProjectile], transform.position + Vector3.forward * _loadedProjectiles[_selectedProjectile].Collider.bounds.max.z, transform.rotation);

        projectile.Rigidbody.AddRelativeForce(projectile.Rigidbody.mass * projectile.ProjectileData.Velocity * gameObject.transform.forward, ForceMode.Impulse);
    }

}
