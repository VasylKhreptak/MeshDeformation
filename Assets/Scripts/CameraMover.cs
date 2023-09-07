using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _speed;

    #region MonoBehaviour

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 right = _transform.right;
        Vector3 forward = _transform.forward;

        Vector3 direction = (right * horizontal + forward * vertical).normalized;

        _transform.position += direction * (_speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            _transform.position += Vector3.up * (_speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _transform.position += Vector3.down * (_speed * Time.deltaTime);
        }
    }

    #endregion
}
