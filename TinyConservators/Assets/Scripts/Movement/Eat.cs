using System;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] float spittingForce;
    [SerializeField] Vector2 spitOffset;
    [SerializeField] float mouthTime;
    [SerializeField] bool swallow;

    IEnumerator mouthTimer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEatable eatObject = collision.GetComponent<IEatable>();
        if(eatObject != null && !IsCarryingFood())
        {
            if (eatObject.Eatable())
            {
                eatObject.Eat(transform.parent.gameObject);
                if (eatObject.Spittable())
                {
                    GetComponentInParent<Player>().AnimationTransition(1,2,0.1f);
                    mouthTimer = MouthTimeExceeded();
                    StartCoroutine(mouthTimer);
                }
                else
                {
                    GetComponentInParent<Player>().AnimationTransition(1, 0, 0.1f);
                }
            }
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
        IKnockoutable knockout = GetComponentInParent<IKnockoutable>();
        if (knockout != null)
        {
            knockout.PauseKnockout(.2f);
        }

        if(mouthTimer != null)
        {
            StopCoroutine(mouthTimer);
            mouthTimer = null;
        }
        eatObject.SpitOut(transform.parent.gameObject);
        GetComponentInParent<Player>().AnimationTransition(1, 0, 0.1f);
        MonoBehaviour mb = eatObject as MonoBehaviour;

        if(mb != null)
        {
            Rigidbody2D eatRB = mb.gameObject.GetComponent<Rigidbody2D>();

            if(eatRB != null)
            {
                //Debug.Break();
                int direction = GetComponentInParent<WalkingMovement>().GetDirection();
                eatRB.gameObject.transform.position += (Vector3)(Vector2.right + spitOffset) * direction;
                eatRB.AddForce(Vector2.right * direction * spittingForce);
                
            }
        }
    }

    void Burp()
    {
        //Debug.Break();
        IEatable eatObject = transform.parent.GetComponentInChildren<IEatable>(true);
        eatObject.SetEatable(false); //Emergency fix
        IKnockoutable knockout = GetComponentInParent<IKnockoutable>();
        if (knockout != null)
        {
            knockout.PauseKnockout(.2f);
        }

        GetComponentInParent<Player>().AnimationTransition(4, 0, 0.2f); //Do a burp animation

        MonoBehaviour mb = eatObject as MonoBehaviour;

        if (mb != null)
        {
            GameObject g = mb.gameObject;
            
            int direction = GetComponentInParent<WalkingMovement>().GetDirection();
            g.transform.position += (Vector3)(Vector2.right + spitOffset) * direction;
        }
        eatObject.Consumed(transform.parent.gameObject);
    }

    IEnumerator MouthTimeExceeded() 
    {
        float time = 0;
        while(time < mouthTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (swallow)
        {
            Burp();
        }
        else
        {
            Spit();
        }
        mouthTimer = null;
        
    }

    
}
