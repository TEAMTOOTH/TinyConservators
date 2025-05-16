using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class BossMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject FindAttackSpot()
    {
        AttackPoint attackSpot = GetComponent<BossAttack>().ChooseNextAttackPoint();
        if(attackSpot == null)
        {
            Debug.Log("Level lost");
        }
        return attackSpot.gameObject;
        
    }

    public void Move(float movementTime, Vector3 startPos, Vector3 endPos)
    {
        StartCoroutine(MakeMove());
        IEnumerator MakeMove()
        {
            float time = 0f;

            while (time < movementTime)
            {
                time += Time.deltaTime;

                //float curveValue = moveCurve.Evaluate(time / movementTime);
                //float overshootFactor = curveValue - 1f;

                // Lerp from start to end with overshoot
                //Vector3 basePosition = Vector3.Lerp(startPos, endPos, Mathf.Clamp01(curveValue));
                Vector3 basePosition = Vector3.Lerp(startPos, endPos, time / movementTime);
                //float bounce = Mathf.Sin(overshootFactor * Mathf.PI) * 50f;
                float bounce = 1f;

                //Vector3 finalPosition = basePosition + Vector3.up * bounce;
                //transform.position = finalPosition;
                transform.position = basePosition;

                yield return null;
            }

            transform.position = endPos;
        }
    }
}
