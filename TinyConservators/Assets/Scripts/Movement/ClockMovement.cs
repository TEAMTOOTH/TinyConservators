using System.Collections;
using UnityEngine;

public class ClockMovement : MonoBehaviour
{

    [SerializeField] Vector2[] positions;
    [SerializeField] float totalDuration;
    [SerializeField] float waitTime;
    [SerializeField] int startPoint;


    [Header("Circle variables")]
    //[SerializeField] bool useCircleTool;
    [SerializeField] int num;
    [SerializeField] Vector3 point;
    [SerializeField] float radius;

    int pointIndex;
    int timeIndex;
    bool canMove = true;
    Rigidbody2D rb;
    float speed;
    float[] speeds;
    float[] times;
    float[] percentages;

    float time;

    //This is definately the right way to do this...
    bool iterate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeMoving(float totalTravelTime, int startIndex, int num, float radius)
    {
        totalDuration = totalTravelTime; //How long it takes to travel around the circle
        startPoint = startIndex; //Where in the circle the object should start
        this.num = num; //How many points a circle should have, higher = smoother.
        this.radius = radius; //How big the circle is

        GenerateCircle();
        rb = GetComponent<Rigidbody2D>();

        //speed = totalSpeed / positions.Length;


        //speed = totalDuration/positions.Length;

        //This is the old one, attemt at new one follows under.
        percentages = new float[positions.Length];
        speeds = new float[positions.Length];
        times = new float[positions.Length];
        float totalDistance = 0;

        for (int i = 0; i < positions.Length; i++)
        {
            if (i < positions.Length - 1)
            {
                totalDistance += Vector2.Distance(positions[i], positions[i + 1]);
            }
        }
        totalDistance += Vector2.Distance(positions[positions.Length - 1], positions[0]);

        for (int i = 0; i < positions.Length; i++)
        {
            if (i < positions.Length - 1)
            {
                percentages[i] = Vector2.Distance(positions[i], positions[i + 1]) / totalDistance;
            }
        }
        percentages[positions.Length - 1] = Vector2.Distance(positions[positions.Length - 1], positions[0]) / totalDistance;

        for (int i = 0; i < positions.Length; i++)
        {
            times[i] = totalDuration * percentages[i];
            //Debug.Log(times[i]);
        }

        float lastElement = times[times.Length - 1]; // Store the last element

        // Shift elements to the right, so that the right position gets the right timing
        for (int i = times.Length - 1; i > 0; i--)
        {
            times[i] = times[i - 1];
        }
        // Place the last element at the top (index 0)
        times[0] = lastElement;



        //speed = 1;

        pointIndex = startPoint;
        if (startPoint >= positions.Length)
        {
            pointIndex = positions.Length - 1;
        }
        transform.localPosition = positions[pointIndex];


        MovePlatform();
    }

    //Part man, part machine, thank you CHATMODDAFOKKINGPT for getting me over the finishline, I wish I had payed attention in math class...
    void MovePlatform()
    {
        StartCoroutine(MovePlatformEnumerator());

        IEnumerator MovePlatformEnumerator()
        {
            pointIndex++;
            if (pointIndex >= positions.Length)
            {
                pointIndex = 0;
            }

            Vector2 endPoint = positions[pointIndex];
            float time = 0f;
            Vector2 startPosition = transform.localPosition;

            float moveDuration = times[pointIndex];

            // While the time hasn't reached the total duration, move the platform
            while (time < moveDuration)
            {
                // Calculate the fraction of time passed (from 0 to 1)
                float t = time / moveDuration;

                // Perform the linear interpolation
                transform.localPosition = Vector2.Lerp(startPosition, endPoint, t);

                // Increment time based on the frame rate
                time += Time.deltaTime;

                // Wait until the next frame
                yield return null;
            }

            // Ensure that we set the final position at the end
            transform.localPosition = endPoint;

            // Pause or perform other actions after the movement completes
            PausePlatform();
        }


    }

    void PausePlatform()
    {
        //pointIndex++;

        MovePlatform();
        //StartCoroutine(PausePlatform());
        IEnumerator PausePlatform()
        {
            canMove = false;
            yield return new WaitForSeconds(waitTime);
            canMove = true;


            MovePlatform();
        }
    }

    public void GenerateCircle()
    {
        Debug.Log("GeneratingCircle");
        point = transform.position;
        positions = CreatePointsAroundCircle();
    }

    Vector2[] CreatePointsAroundCircle()
    {
        Vector2[] movePoints = new Vector2[num];
        for (int i = 0; i < num; i++)
        {

            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, vertical);

            /* Get the spawn position */
            movePoints[i] = transform.localPosition + spawnDir * radius; // Radius is just the distance away from the point
        }
        return movePoints;
    }
}
