using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;

    Rigidbody2D rb2D;
    WalkingMovement movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        movement = GetComponent<WalkingMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.GetDirection() < 0)
        {
            sprites[0].flipX = true;
        }
        else if (movement.GetDirection() > 0)
        {
            sprites[0].flipX = false;
        }
    }

    /// <summary>
    /// IncrediblySimplified for test. Not how I want it in the final one. Really needs to be fixed before launch.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="visualIndex"></param>
    public void UpdatePart(int visualIndex)
    {
        //this is also terrible!!!
        sprites[0].GetComponent<CustomizePart>().SetPart(visualIndex);
    }

    public void PlayAnimationIfHasState(string state)
    {
        var anim = GetComponentInChildren<Animator>();

        if (anim != null)
        {
            var stateId = Animator.StringToHash(state);
            var hasState = GetComponentInChildren<Animator>().HasState(0, stateId);

            if (hasState)
            {
                anim.Play(state);
            }
        }

    }
}
