using UnityEngine;

public class WrapAround : MonoBehaviour
{
    Vector2 playAreaCenter = Vector2.zero;
    float halfWidth = 9.5f;
    float fallY = -10f;  // Y threshold for reset
    Vector2 respawnPosition = Vector2.zero; // Where the player respawns

    void CheckOutOfBounds()
    {
        Vector2 pos = transform.position;
        Vector2 newPos = pos;

        // --- Horizontal wrap ---
        float left = playAreaCenter.x - halfWidth;
        float right = playAreaCenter.x + halfWidth;

        if (pos.x > right)
            newPos.x = left;
        else if (pos.x < left)
            newPos.x = right;

        // --- Fall reset ---
        if (pos.y < fallY)
        {
            newPos = respawnPosition;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Stop momentum on respawn
        }

        if (newPos != pos)
            transform.position = newPos;
    }

    void FixedUpdate() //Dont need to check every frame, so adding it to fixed update to check 30times per second
    {
        CheckOutOfBounds();
    }
}
