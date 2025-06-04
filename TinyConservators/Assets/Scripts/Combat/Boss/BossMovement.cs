using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class BossMovement : MonoBehaviour
{
    GameObject[] scatterPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scatterPoints = GameObject.FindGameObjectsWithTag("ScatterPoint");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject FindAttackSpot()
    {
        AttackPoint attackSpot = GetComponent<BossAttack>().ChooseNextAttackPoint();
        if (attackSpot == null)
        {
            LevelFlowManager lFM = GameObject.FindGameObjectWithTag("LevelFlow").GetComponent<LevelFlowManager>();
            lFM.JumpToFlow(lFM.GetAmountOfFlows() - 1);
            Debug.Log("Level lost");

            return null;
        }
        return attackSpot.gameObject;
    }

    public GameObject GetRandomScatterPoint()
    {
        if(scatterPoints.Length > 0)
        {
            return scatterPoints[Random.Range(0, scatterPoints.Length)];
        }

        return null;
    }



    public GameObject GetFurthestAwayScatterPoint(Vector3 from)
    {
        GameObject furthest = null;
        float longestDistance = 0f;

        foreach (GameObject obj in scatterPoints)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(from, obj.transform.position);
            if (distance > longestDistance)
            {
                longestDistance = distance;
                furthest = obj;
            }
        }
        return furthest;
    }

    public GameObject GetClosestScatterPoint(Vector3 from)
    {

        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        

        foreach (GameObject obj in scatterPoints)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(from, obj.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = obj;
            }
        }
        return closest;
    }

    public void MoveTowardsScreen(float moveTime)
    {
        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        Vector3 endPos = new Vector3(-8, -37, 0);
        Vector3 endScale = new Vector3(100, 100, 1);

        StartCoroutine(MakeMove());

        IEnumerator MakeMove()
        {
            float time = 0f;
            while (time < moveTime)
            {
                time += Time.deltaTime;
                transform.localScale = new Vector3();

                Vector3 basePosition = Vector3.Lerp(startPos, endPos, time / moveTime);
                transform.position = basePosition;
                Vector3 baseScale = Vector3.Lerp(startScale, endScale, time / moveTime);
                transform.localScale = baseScale;
                yield return null;
            }

            transform.position = endPos;
            transform.localScale = endScale;
            
            
        }
    }

    public void Move(float baseMoveTime, Vector3 startPos, Vector3 endPos)
    {
        StartCoroutine(MakeMove());

        

        IEnumerator MakeMove()
        {
            float time = 0f;

            float distance = Vector2.Distance(startPos, endPos);

            while (time < baseMoveTime)
            {
                time += Time.deltaTime;
                
                Vector3 basePosition = Vector3.Lerp(startPos, endPos, time/baseMoveTime);
                transform.position = basePosition;

                yield return null;
            }

            transform.position = endPos;
            
        }
    }

   
}
