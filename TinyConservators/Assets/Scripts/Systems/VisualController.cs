using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;

    Rigidbody2D rb2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb2D.linearVelocityX < 0)
        {
            sprites[0].flipX = true;
        }
        else if (rb2D.linearVelocityX > 0)
        {
            sprites[0].flipX = false;
        }
    }
}
