using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingSprite : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.5f, 0f);
    private Material material;
    private Vector2 offset;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        material.SetVector("_Offset", offset);
    }
}
