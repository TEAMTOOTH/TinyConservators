using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomizer : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] CustomizePart[] visualElements;

    

    bool canCustomize = true;

    Vector2 movementInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movementInput.x > sensitivity)
        {
            IncrementSelection(1);
        }
        else if(movementInput.x < -sensitivity)
        {
            IncrementSelection(-1);
        }
    }
    
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnSelect(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //Should progress to next customize step in the game, for now just "finishes" the customization so that we can test 24.04.2025
            Debug.Log("Pressed selection button");
            GetComponentInParent<Player>().DoneCustomizing();
        }
    }

    //Currently working temp
    public void Initialize(int id)
    {
        Debug.Log("Initializing " + id);
        visualElements = GetComponentsInChildren<CustomizePart>();
        foreach (CustomizePart cp in visualElements)
        {
            cp.ChangePart(id);
        }
    }

    void IncrementSelection(int direction)
    {
        Debug.Log(direction);
    }
}
