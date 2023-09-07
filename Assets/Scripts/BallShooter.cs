using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _spawnPointDistance = 1f;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _bulletLifetime = 10f;

    #region MonoBehaviour

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    #endregion

    private void Shoot()
    {
        Vector3 spawnPoint = _transform.position + _transform.forward * _spawnPointDistance;

        GameObject ball = Instantiate(_prefab, spawnPoint, Quaternion.identity);

        if (ball.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = _transform.forward * _speed;
        }

        Destroy(ball, _bulletLifetime);
    }
}
