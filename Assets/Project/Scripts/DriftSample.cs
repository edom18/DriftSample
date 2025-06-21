using TMPro;
using UnityEngine;

public class DriftSample : MonoBehaviour
{
    /// 【計算メモ】
    /// dv / dt = a - kv
    ///     dv / dt ... 速度の微分
    ///     a ... 加速度
    ///     k ... 摩擦係数
    ///     v ... 速度
    /// ここで dv / dt = 0 と置くと、
    /// 0 = a - kv_terget => v_target = a / k
    ///
    /// a: km/s^2
    /// k: 速度に比例した減速（-kv）を与えるため 1/s = s^-1
    ///
    /// よって、v_terget = a / k
    /// km/s^2 / s^-1 = km/s として、単位が一致
    [SerializeField] private float _targetSpeed = 10f;

    [SerializeField] private float _acc = 2f;
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private float _turnRotateSpeed = 1.0f;
    [SerializeField] private float _burstScale = 2f;
    [SerializeField] private float _dragScale = 2f;
    [SerializeField] private KeyCode _burstKey = KeyCode.W;
    [SerializeField] private KeyCode _breakKey = KeyCode.Space;

    [SerializeField] private TMP_Text _velocityText;
    [SerializeField] private TMP_Text _burstText;
    [SerializeField] private TMP_Text _breakText;

    private float Drag => _acc / _targetSpeed;

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

        Vector3 forward = transform.forward;

        float acceleration = _acc;
        if (Input.GetKey(_burstKey))
        {
            acceleration *= _burstScale;

            _burstText.text = "on";
        }
        else
        {
            _burstText.text = "off";
        }

        float drag = Drag;
        if (Input.GetKey(_breakKey))
        {
            drag *= _dragScale;

            _breakText.text = "on";
        }
        else
        {
            _breakText.text = "off";
        }

        _velocoty += forward * (acceleration * Time.deltaTime);
        _velocoty = Vector3.RotateTowards(_velocoty, forward * _velocoty.magnitude, _rotateSpeed * Time.deltaTime, 0);

        _velocoty -= _velocoty * (drag * Time.deltaTime);
        transform.position += _velocoty * Time.deltaTime;

        _velocityText.text = $"{_velocoty.magnitude:F2} km/s";
    }
}