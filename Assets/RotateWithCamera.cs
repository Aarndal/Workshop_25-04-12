using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _rotatingObject;

    private void Awake()
    {
        if (gameObject.transform.rotation != _rotatingObject.transform.rotation)
            gameObject.transform.SetPositionAndRotation(transform.position, _rotatingObject.transform.rotation);
    }

    private void Update()
    {
        if (gameObject.transform.rotation != _rotatingObject.transform.rotation)
            gameObject.transform.SetPositionAndRotation(transform.position, _rotatingObject.transform.rotation);
    }
}
