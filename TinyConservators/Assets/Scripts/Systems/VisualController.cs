using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] float initialScale;

    Rigidbody2D rb2D;
    WalkingMovement movement;

    GameObject visualObject;

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
        if (visualObject == null)
            return;

        visualObject.transform.localScale = new Vector3(initialScale * movement.GetDirection(), visualObject.transform.localScale.y, visualObject.transform.localScale.z);

        /*if (movement.GetDirection() < 0)
        {
            
        }
        else if (movement.GetDirection() > 0)
        {
            transform.localScale = new Vector3(initaltransform.localScale.x, transform.localScale.y, transform.localScale.z);
        }*/
    }

    /// <summary>
    /// IncrediblySimplified for test. Not how I want it in the final one. Really needs to be fixed before launch.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="visualIndex"></param>
    public void UpdatePart(int visualIndex)
    {
        //this is also terrible!!!
        //sprites[0].GetComponent<CustomizePart>().SetPart(visualIndex);
    }

    public void SetVisualObjectVisibility(bool state)
    {
        if(visualObject != null)
        {
            visualObject.SetActive(state);
            
            if (state == true)
            {
                //I have no idea why, but it seems that maybe resetting the items that are active as you spawn saves you from losing the face.
                //SpriteRenderer[] activeSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

                //GetComponentInChildren<Animator>()?.Play("Float");
                //for(int i = 0; i < activeSpriteRenderers.Length; i++)
                //{
                    //activeSpriteRenderers[i].gameObject.SetActive(false);
                    //activeSpriteRenderers[i].gameObject.SetActive(true);
                //}

            }
        }

        
    }

    public void SetVisualObject(GameObject visual)
    {
        visualObject = visual;
        initialScale = visual.transform.localScale.x;
        
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
