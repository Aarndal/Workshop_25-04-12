using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsible for health management.
/// </summary>
public abstract class Health : MonoBehaviour
{
    public event Action HealthChanged;
    public event Action HealthDecreased;
    public event Action HealthIncreased;

    [Header("Values")]
    [SerializeField, Min(1)]
    protected int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        protected set
        {
            if (_currentHealth != value)
            {
                HealthChanged?.Invoke();

                if (value < _currentHealth)
                    HealthDecreased?.Invoke();

                if (value > _currentHealth)
                    HealthIncreased?.Invoke();

                _currentHealth = value;
            }
        }
    }

    public abstract void IncreaseCurrentHealth(int amount);

    public virtual void DecreaseCurrentHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, int.MaxValue);

        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }
}