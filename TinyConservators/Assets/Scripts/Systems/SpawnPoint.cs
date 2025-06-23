using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Vector3 fromPos;
    [SerializeField] float moveTime;
    [SerializeField] float pushForce;
    Vector3 startPos;

    private Camera mainCamera;
   
    private void Awake()
    {
        startPos = transform.position;
        mainCamera = Camera.main; //I know this is a no no, but i also know that this is being called 6 times in the beginning of a level and in the whole of it won't matter too much.
        fromPos = new Vector3(fromPos.x * GetScreenSide(), startPos.y, startPos.z);
        transform.position = new Vector3(fromPos.x, startPos.y, startPos.z);
    }

    public void Spawn(GameObject p)
    {
        GameObject player = p;
        StartCoroutine(ShootPlayerOut());
        
        IEnumerator ShootPlayerOut()
        {
            float elapsedTime = 0f;

            while (elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(fromPos, startPos, elapsedTime / moveTime); //a bit confusing naming scheme here, but it indicates where the object started and hence is going back to.
                yield return null;
            }
            transform.position = startPos;

            player.transform.position = startPos;
            player.GetComponent<Player>().FullFreeze(false);
            //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetScreenSide() * -1 * pushForce, 0));
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * pushForce, 0));
        }

    }


    public int GetScreenSide()
    {
        

        // Convert world position to screen position
        Vector3 screenPos = mainCamera.WorldToScreenPoint(startPos);
        float screenCenter = Screen.width / 2f;

        if (screenPos.x < screenCenter - 1f) return -1;
        else return 1;
        
    }
}
