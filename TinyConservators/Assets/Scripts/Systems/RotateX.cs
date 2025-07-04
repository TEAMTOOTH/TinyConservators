using UnityEngine;

public class RotateX : MonoBehaviour
{
    public float rotationSpeed = 45f; // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
