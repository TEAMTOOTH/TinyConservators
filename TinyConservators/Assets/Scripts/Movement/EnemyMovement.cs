using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool canMove = true;
    Rigidbody2D rb2D;
    bool lookForTarget = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookForTarget)
        {
            FindTarget();
        }
        
        
    }

    public void StartMoving()
    {
        SetCanMove(true);
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        transform.rotation = Quaternion.identity;
        GetComponent<AIPath>().enabled = true;
        rb2D.freezeRotation = true;
    }

    void FindTarget()
    {
        float searchWidth = 10;
        float searchHeight = 6;
        float shortestDistance = searchWidth+searchHeight;

        GameObject targetObject = gameObject;

        Collider2D[] objectsFound = Physics2D.OverlapAreaAll(new Vector2(-searchWidth, -searchHeight), new Vector2(searchWidth, searchHeight));
        for(int i = 0; i < objectsFound.Length; i++)
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

    public void SetNewTarget(GameObject newTarget)
    {
        GetComponent<AIDestinationSetter>().target = newTarget.transform;
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public void SetLookForTarget(bool state)
    {
        lookForTarget = state;
    }

    public void ChangeSpeed(float newSpeed)
    {
        GetComponent<AIPath>().maxSpeed = newSpeed;
    }
}
