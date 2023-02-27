using UnityEngine;

public class AvoidSurfaceFall : MonoBehaviour
{
    public bool hasGrabbed { set; get; }

    private Vector3 _initialPosition;
    private void Start()
    {
        _initialPosition = transform.localPosition;
    }


    private void LateUpdate()
    {
        if (!hasGrabbed && transform.localPosition.y < -2.5f)
        {
            transform.localPosition = _initialPosition;
        }
    }
}
