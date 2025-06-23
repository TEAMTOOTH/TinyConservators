using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInputLogger : MonoBehaviour
{
    Vector2 movementInput;

    public void Fly(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Fly");
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    private void Update()
    {
        if(movementInput.magnitude > 0.1f)
        {
            Debug.Log(movementInput);
        }
    }
}
