using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FlyingMovement : MonoBehaviour
{
    [SerializeField] float maxFlightSpeed;
    [SerializeField] float impulseForce;
    [SerializeField] int amountOfFlapsPerFly;
    [SerializeField] float flapCooldown;
    [SerializeField] Player player;
    Rigidbody2D rb2D;
    int amountOfFlaps;

    bool isButtonBeingHeldDown;

    bool canFlap = true;
    bool allowedToFlap = true; //A general lock on flapping, while can flap is used as local/short term check.

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        amountOfFlaps = amountOfFlapsPerFly;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isButtonBeingHeldDown)
        {
            HoldFlying();
        }
    }

    public void Fly(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GetComponent<Player>().InputRegistered();
            if(amountOfFlaps > 0 && canFlap && allowedToFlap)
            {
                player?.SetGrounded(false);
                rb2D.AddForceY(impulseForce, ForceMode2D.Impulse);
                if(rb2D.linearVelocityY > maxFlightSpeed)
                {
                    rb2D.linearVelocityY = maxFlightSpeed;
                }
                amountOfFlaps--;

                //flap SFX
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/flap");

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

    public void HoldFlying()
    {
        
            if (amountOfFlaps > 0 && canFlap && allowedToFlap)
            {
                rb2D.AddForceY(impulseForce, ForceMode2D.Impulse);
                if (rb2D.linearVelocityY > maxFlightSpeed)
                {
                    rb2D.linearVelocityY = maxFlightSpeed;
                }
                amountOfFlaps--;

                StartCoroutine(ResetCanFlap());
                IEnumerator ResetCanFlap()
                {
                    canFlap = false;
                    float timeElapsed = 0;
                    while (timeElapsed < flapCooldown)
                    {
                        timeElapsed += Time.deltaTime;
                        yield return null;
                    }
                    canFlap = true;
                }
            }

        
    }

    public void HoldToFly(InputAction.CallbackContext ctx)
    {

        if (ctx.started)
        {
            isButtonBeingHeldDown = true;
        }
        else if (ctx.canceled)
        {
            isButtonBeingHeldDown = false;
        }
    }


    public void ResetFlaps()
    {
       
        amountOfFlaps = amountOfFlapsPerFly;
    }

    public void SetAllowedToFlap(bool state)
    {
        allowedToFlap = state;
    }
}
