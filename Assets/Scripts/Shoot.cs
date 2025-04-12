using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private int _selectedProjectile = 0;
    [SerializeField]
    private Projectile[] _loadedProjectiles;

    private InputAction _attack;
    private InputAction _next;
    private InputAction _previous;

    private void Awake()
    {
        _attack = InputSystem.actions.FindAction("Attack");
        _next = InputSystem.actions.FindAction("Next");
        _previous = InputSystem.actions.FindAction("Previous");
    }

    private void OnEnable()
    {
        _attack.performed += OnAttackPerformed;
        _previous.performed += OnNextPerformed;
        _next.performed += OnPreviousPerformed;
    }


    private void OnDisable()
    {
        _next.performed -= OnNextPerformed;
        _previous.performed -= OnPreviousPerformed;
        _attack.performed -= OnAttackPerformed;
    }

    private void OnNextPerformed(InputAction.CallbackContext context)
    {
        _selectedProjectile += 1;

        if (_selectedProjectile > _loadedProjectiles.Length - 1)
            _selectedProjectile = 0;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        ShootProjectile();
    }

    private void OnPreviousPerformed(InputAction.CallbackContext context)
    {
        _selectedProjectile -= 1;

        if (_selectedProjectile < 0)
            _selectedProjectile = _loadedProjectiles.Length - 1;
    }

    public void ShootProjectile()
    {
        //Vector3 direction = new();

        Projectile projectile = Instantiate(_loadedProjectiles[_selectedProjectile], transform.position, transform.rotation);

        //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        //    direction = (hitInfo.transform.position - transform.position).normalized;
        //else
        //    direction = transform.forward;

        projectile.Rigidbody.AddForce(projectile.ProjectileData.Mass * projectile.ProjectileData.Velocity * transform.forward, ForceMode.Impulse);
    }

}
