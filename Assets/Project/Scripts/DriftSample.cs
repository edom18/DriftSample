using UnityEngine;

public class DriftSample : MonoBehaviour
{
    [SerializeField] private float _acc = 2f;
    [SerializeField] private float _drag = 0.99f;
    [SerializeField] private float _rotateSpeed = 1f;

    private Vector3 _velocoty = Vector3.zero;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -_rotateSpeed, Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, _rotateSpeed, Space.Self);
        }

        _velocoty *= _drag;
        _velocoty += transform.forward * (_acc * Time.deltaTime);
        transform.position += _velocoty * Time.deltaTime;
    }
}
