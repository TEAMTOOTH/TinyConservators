using UnityEngine;

public class RotateZ : MonoBehaviour
{
    public float rotationSpeed = 45f; // degrees per second

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
