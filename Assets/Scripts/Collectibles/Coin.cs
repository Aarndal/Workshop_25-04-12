using UnityEngine;

public class Coin : MonoBehaviour, IAmCollectible, IAmMovable
{
    [SerializeField]
    private float _rotationAngle;

    public bool Collect()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        transform.Rotate(0.0f, _rotationAngle, 0.0f);
    }
}
