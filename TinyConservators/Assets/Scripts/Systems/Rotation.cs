using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float rotationSpeed = 45f; // degrees per second

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}

