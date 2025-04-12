using UnityEditor.ShaderGraph.Configuration;
using UnityEngine;
using UnityEngine.AI;

public interface IAmAutonomousMovable
{
    public bool ReachedTarget { get; set; }
    public float DistanceToTarget { get; }
    public float MinDistanceToTarget { get; set; }
    public Vector3 CurrentPosition { get; }

    void MoveTo(TargetProvider targetProvider);
}