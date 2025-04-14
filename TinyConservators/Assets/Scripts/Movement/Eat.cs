using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] float spittingForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEatable eatObject = collision.GetComponent<IEatable>();
        if(eatObject != null && !IsCarryingFood())
        {
            eatObject.Eat(transform.parent.gameObject);
        }
    }

    public bool IsCarryingFood()
    {
        IEatable eatObject = transform.parent.GetComponentInChildren<IEatable>(true);
        //IEatable eatObject = GetComponentInChildren<IEatable>(true);
        if(eatObject == null)
            return false;
        return true;
    }

    public void Spit()
    {
        IEatable eatObject = transform.parent.GetComponentInChildren<IEatable>(true);

        eatObject.SpitOut(transform.parent.gameObject);
        MonoBehaviour mb = eatObject as MonoBehaviour;

        if(mb != null)
        {
            Rigidbody2D eatRB = mb.gameObject.GetComponent<Rigidbody2D>();

            if(eatRB != null)
            {
                //eatRB.AddForce(Vector2.right * direction * spittingForce);
                eatRB.AddForce(Vector2.right * GetComponentInParent<WalkingMovement>().GetDirection() * spittingForce);
            }

        }
        
    }
}
