using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool canMove = true;
    Rigidbody2D rb2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockOut()
    {
        canMove = false;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void StartMoving()
    {
        canMove = true;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        transform.rotation = Quaternion.identity;
    }
}
