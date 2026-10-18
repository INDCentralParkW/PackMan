using UnityEngine;

[ExecuteAlways]
public class Colliders : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null) return;

        Gizmos.color = Color.green;

        // Get the collider's world position and size
        Vector2 offset = collider.offset;
        Vector2 size = collider.size;

        // Apply rotation and position
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;

        // Draw a wire cube for the 2D box
        Gizmos.DrawWireCube(offset, size);
    }
}