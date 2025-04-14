using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingMovement : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector2 movementInput;
    Vector2 velocity;
    int direction = 1;
    bool canMove = true;

    [SerializeField] Vector2 targetVelocity;
    [SerializeField] float horizontalSpeedIncrease = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        //Calculate final velocity of rigidbody after movement and jump/jetpack has been applied
        if (canMove)
        {
            rb2D.linearVelocity = new Vector2(velocity.x, rb2D.linearVelocityY);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(0, 0); //For the test on 07.04, change later
        }
            
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    //Take in input and apply it to the player/Handle all the game feel in terms of left/right movement within this class
    void HandleMovement()
    {
        if (!canMove) 
        {
            return;
        }
     

        if (movementInput.x != 0)
        {
            direction = 1;
            
            if (movementInput.x < 0)
            {
                direction = -1;
            }

            float directionX = Mathf.Sign(movementInput.x);
            float inputMagnitudeX = Mathf.Abs(movementInput.x); // Get how far the stick is pushed (0 to 1)

            // If changing directionX, allow a small slide before reversing
            if (Mathf.Sign(velocity.x) != directionX && velocity.x != 0)
            {
                // Apply stronger deceleration when switching directionXs
                velocity.x = Mathf.MoveTowards(velocity.x, 0, (horizontalSpeedIncrease * 2) * Time.deltaTime);
            }
            else
            {
                // Accelerate based on input magnitude (makes it smoother for controllers)
                float targetSpeed = targetVelocity.x * inputMagnitudeX;
                if (Mathf.Abs(velocity.x) < targetSpeed)
                    velocity.x += directionX * horizontalSpeedIncrease * inputMagnitudeX * Time.deltaTime;
                else
                    velocity.x = directionX * targetSpeed;
            }
        }
        else
        {
            // Apply deceleration when no input is given
            velocity.x = Mathf.MoveTowards(velocity.x, 0, horizontalSpeedIncrease * Time.deltaTime);
        }
    }

    public void SetSpeedParameters((float, float) speeds)
    {
        targetVelocity.x = speeds.Item1;
        horizontalSpeedIncrease = speeds.Item2;
    }

    public (float, float) GetSpeedParameters()
    {
        return (targetVelocity.x, horizontalSpeedIncrease);
    }

    public int GetDirection()
    {
        if (0 < velocity.x)
            return 1;
        return -1;
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }
}
