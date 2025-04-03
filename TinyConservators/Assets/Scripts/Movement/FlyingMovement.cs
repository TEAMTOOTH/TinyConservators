using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FlyingMovement : MonoBehaviour
{
    [SerializeField] float maxFlightSpeed;
    [SerializeField] float impulseForce;
    [SerializeField] int amountOfFlapsPerFly;
    [SerializeField] float flapCooldown;
    Rigidbody2D rb2D;
    int amountOfFlaps;

    bool canFlap = true;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        amountOfFlaps = amountOfFlapsPerFly;
    }

    public void Fly(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(amountOfFlaps > 0 && canFlap)
            {
                rb2D.AddForceY(impulseForce, ForceMode2D.Impulse);
                if(rb2D.linearVelocityY > maxFlightSpeed)
                {
                    rb2D.linearVelocityY = maxFlightSpeed;
                }
                amountOfFlaps--;

                StartCoroutine(ResetCanFlap());
                IEnumerator ResetCanFlap()
                {
                    canFlap = false;
                    float timeElapsed = 0;
                    while(timeElapsed < flapCooldown)
                    {
                        timeElapsed += Time.deltaTime;
                        yield return null;
                    }
                    canFlap = true;
                }
            }
            
        }
    }

    public void ResetFlaps()
    {
       
        amountOfFlaps = amountOfFlapsPerFly;
    }
}
