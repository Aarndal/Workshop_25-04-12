using System;
using UnityEngine;

[Serializable]
public abstract class TargetProvider : MonoBehaviour
{
    //public event Action TargetDetected;
    //public event Action TargetLost;

    public Transform Target { get; protected set; }
    public bool HasTarget
    {
        get
        {
            return Target != null && Target.gameObject.activeInHierarchy;

            //if (Target != null && Target.gameObject.activeInHierarchy)
            //{
            //    TargetDetected?.Invoke();
            //    return true;
            //}
            //else
            //{
            //    TargetLost?.Invoke();
            //    return false;
            //}
        }
    }
    public float SqrDistanceToTarget
    {
        get
        {
            if (HasTarget)
                return (Target.position - transform.parent.transform.position).sqrMagnitude;
            return float.MaxValue; // Maybe change this to null or 0.0f?
        }
    }

    public abstract Transform GetTarget();
}