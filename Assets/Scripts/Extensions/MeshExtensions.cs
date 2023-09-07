using UnityEngine;

namespace Extensions
{
    public static class MeshExtensions
    {
        public static Vector3[] GetWorldVertices(this Mesh mesh, Transform transform)
        {
            Vector3[] vertices = mesh.vertices;
            Vector3[] worldVertices = new Vector3[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                worldVertices[i] = transform.TransformPoint(vertices[i]);
            }

            return worldVertices;
        }
    }
}
