using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool canMove = true;
    Rigidbody2D rb2D;
    bool lookForTarget = true;

    GameObject[] hoverPoints;

    AIPath ai;

    float initialScale;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        hoverPoints = GameObject.FindGameObjectsWithTag("HoverPoint");
        ai = GetComponent<AIPath>();

        initialScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookForTarget)
        {
            FindTarget();
        }

        if(ai.velocity.x < 0)
        {
            transform.localScale = new Vector3(-initialScale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initialScale, transform.localScale.y, transform.localScale.z);
        }

        Debug.Log(ai.velocity);
    }

    public void StartMoving()
    {
        SetCanMove(true);
        //rb2D.bodyType = RigidbodyType2D.Kinematic;
        transform.rotation = Quaternion.identity;
        GetComponent<AIPath>().enabled = true;
        rb2D.freezeRotation = true;
    }

    void FindTarget()
    {
        float searchWidth = 20;
        float searchHeight = 15;
        float shortestDistance = searchWidth+searchHeight;

        GameObject targetObject = GetFurthestAwayPoint(transform.position, hoverPoints);

        Collider2D[] objectsFound = Physics2D.OverlapAreaAll(new Vector2(-searchWidth, -searchHeight), new Vector2(searchWidth, searchHeight));
        if (lookForTarget)
        {
            for (int i = 0; i < objectsFound.Length; i++)
            {
                if (objectsFound[i].gameObject.CompareTag("Player"))
                {
                    IKnockoutable knockout = objectsFound[i].GetComponent<IKnockoutable>();
                    if (knockout != null && !knockout.IsKnockedOut())
                    {
                        float distance = Vector3.Distance(transform.position, objectsFound[i].transform.position);
                        if (shortestDistance > distance)
                        {
                            shortestDistance = distance;
                            targetObject = objectsFound[i].gameObject;
                        }
                    }
                }
            }
            SetNewTarget(targetObject);
        }
        
    }

    public void SetNewTarget(GameObject newTarget)
    {
        GetComponent<AIPath>().destination = newTarget.transform.position;
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public GameObject GetFurthestAwayPoint(Vector3 from, GameObject[] list)
    {
        GameObject furthest = null;
        float longestDistance = 0f;

        foreach (GameObject obj in hoverPoints)
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

    public void SetLookForTarget(bool state)
    {
        lookForTarget = state;
    }

    public void ChangeSpeed(float newSpeed)
    {
        GetComponent<AIPath>().maxSpeed = newSpeed;
    }

    public float GetCurrentSpeed()
    {
        return GetComponent<AIPath>().maxSpeed;
    }
}
