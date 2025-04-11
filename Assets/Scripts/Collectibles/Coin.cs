using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Coin : MonoBehaviour, IAmCollectible, IAmMovable
{
    [SerializeField, Min(1)]
    private uint _points = 1;
    [SerializeField, Range(10.0f, 90.0f)]
    private float _rotationAngle = 60.0f;

    private SphereCollider _collider = default;

    public static Action<uint> CoinCollected;

    private void Awake()
    {
        if (!gameObject.TryGetComponent(out _collider))
        {
            _collider = GetComponentInChildren<SphereCollider>();

            if (_collider == null)
            {
                _collider = gameObject.AddComponent<SphereCollider>();
            }
        }
    }

    private void Start()
    {
        _collider.isTrigger = true;
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collect(other.transform);
    }

    public void Move()
    {
        transform.Rotate(0.0f, _rotationAngle * Time.deltaTime, 0.0f);
    }

    public bool Collect(Transform collector)
    {
        StartCoroutine(CollectAndDestroy());
        return true;
    }

    private IEnumerator CollectAndDestroy()
    {
        CoinCollected?.Invoke(_points);
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
}
