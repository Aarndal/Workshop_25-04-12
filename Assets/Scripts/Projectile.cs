using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private SOProjectile _projectileData = default;

    private float _lifeTime = 0.0f;
    private Collider _collider = default;
    private Rigidbody _rigidbody = default;

    public Collider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    public SOProjectile ProjectileData => _projectileData;

    private void Awake()
    {
        if(!gameObject.TryGetComponent(out _collider))
        {
            _collider = GetComponentInChildren<Collider>();
            _collider = _collider != null ? _collider : gameObject.AddComponent<SphereCollider>();
        }

        if (!gameObject.TryGetComponent(out _rigidbody))
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _rigidbody = _rigidbody != null ? _rigidbody : gameObject.AddComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        _rigidbody.mass = _projectileData.Mass;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (_lifeTime >= _projectileData.MaxLifeTime)
            DestroyProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IAmDamageable target))
            target.TakeDamage(_projectileData.Damage);

        if (_projectileData.DestroyOnContact && collision.gameObject != gameObject)
            DestroyProjectile();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IAmDamageable target))
            target.TakeDamage(_projectileData.Damage);

        if (_projectileData.DestroyOnContact && collision.gameObject != gameObject)
            DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
