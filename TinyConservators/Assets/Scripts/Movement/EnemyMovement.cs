using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool canMove = true;
    Rigidbody2D rb2D;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
    }

    public void KnockOut()
    {
        canMove = false;
        GetComponent<AIPath>().enabled = false;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        rb2D.freezeRotation = false;
    }

    public void StartMoving()
    {
        canMove = true;
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


        Collider2D[] objectsFound = Physics2D.OverlapAreaAll(new Vector2(-searchWidth, -searchHeight), new Vector2(searchWidth, searchHeight));
        for(int i = 0; i < objectsFound.Length; i++)
        {
            if (objectsFound[i].gameObject.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, objectsFound[i].transform.position);
                if(shortestDistance > distance)
                {
                    shortestDistance = distance;
                    SetNewTarget(objectsFound[i].gameObject);
                }
            }
        }
    }

    void SetNewTarget(GameObject newTarget)
    {
        GetComponent<AIDestinationSetter>().target = newTarget.transform;
    }
}
