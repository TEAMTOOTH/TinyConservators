using UnityEngine;

public class SpinZMovement : MonoBehaviour
{
    [Header("Rotation Speed in Degrees per Second")]
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate around Z axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
