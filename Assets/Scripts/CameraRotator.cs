using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;

    #region MonoBehaviour

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        _rotationX -= vertical * _verticalSpeed;
        _rotationY += horizontal * _horizontalSpeed;

        _rotationX = Mathf.Clamp(_rotationX, _minAngle, _maxAngle);

        _transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
    }

    #endregion
}
