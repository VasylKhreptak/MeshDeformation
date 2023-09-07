using Extensions;
using UnityEngine;

public class CPU_MeshDeformator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshCollider _meshCollider;

    [Header("Preferences")]
    [SerializeField] private float _minImpulse;
    [SerializeField] private float _maxImpulse;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _minVertexDisplacement;
    [SerializeField] private float _maxVertexDisplacement;
    [SerializeField] private AnimationCurve _radiusCurve;
    [SerializeField] private AnimationCurve _vertexDisplacementCurve;
    [SerializeField] private AnimationCurve _fadeoutCurve;

    private Mesh _mesh;
    private Vector3[] _initialVertices;
    private Vector3[] _vertices;

    #region MonoBehaviour

    private void OnValidate()
    {
        _meshFilter ??= GetComponent<MeshFilter>();
        _meshCollider ??= GetComponent<MeshCollider>();
    }

    private void Awake()
    {
        _mesh = _meshFilter.mesh;
        _initialVertices = _mesh.vertices;
        _vertices = _mesh.vertices;
    }

    #endregion

    private void OnCollisionEnter(Collision other)
    {
        int contactCount = other.contactCount;

        for (int i = 0; i < contactCount; i++)
        {
            ContactPoint contact = other.GetContact(i);

            Vector3 direction = -other.impulse.normalized;
            float impulse = other.impulse.magnitude;

            Deform(contact.point, direction, impulse);
        }
    }

    private void Deform(Vector3 contactPoint, Vector3 direction, float impulse)
    {
        if (impulse < _minImpulse) return;

        float radius = _radiusCurve.Evaluate(_minImpulse, _maxImpulse, impulse, _minRadius, _maxRadius);
        float maxVertexDisplacement = _vertexDisplacementCurve.Evaluate(_minImpulse, _maxImpulse, impulse,
            _minVertexDisplacement, _maxVertexDisplacement);

        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector3 worldVertex = transform.TransformPoint(_vertices[i]);

            float distance = Vector3.Distance(contactPoint, worldVertex);

            if (distance <= radius)
            {
                float normalizedDistance = distance / radius;
                float fadeout = _fadeoutCurve.Evaluate(normalizedDistance);
                float normalizedVertexDisplacement = maxVertexDisplacement * fadeout;

                worldVertex += direction * normalizedVertexDisplacement;

                _vertices[i] = transform.InverseTransformPoint(worldVertex);
            }
        }

        _mesh.vertices = _vertices;
        // _mesh.RecalculateNormals();
        _meshCollider.sharedMesh = _mesh;
    }
}
