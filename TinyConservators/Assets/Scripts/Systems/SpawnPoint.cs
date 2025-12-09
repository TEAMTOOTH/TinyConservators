using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Vector3 fromPos;
    [SerializeField] float moveTime;
    [SerializeField] float pushForce;
    [SerializeField] Transform spawnPoint;
    Vector3 startPos;

    private Camera mainCamera;
   
    private void Awake()
    {
        startPos = transform.position;
        mainCamera = Camera.main; //I know this is a no no, but i also know that this is being called 2 times in the beginning of a level and in the whole of it won't matter too much.
  
    }

    public void Spawn(GameObject p)
    {
        GameObject player = p;

        int playerId = p.GetComponent<Player>().GetPlayerId();

        //Getting the player ID so that the players dont spawn inside eachother, but have an offset based on their playerID;


        if(playerId > 2)
        {
            playerId -= 3;
        }

        int spawnOffset = playerId - 1;
        //Debug.Log(p.GetComponent<Player>().GetPlayerId() + ", " + gameObject.name + " : " + spawnOffset);

        player.transform.position = spawnPoint.position + new Vector3(0, spawnOffset);
        p.GetComponent<Player>().GetPlayerId();
        player.GetComponent<Player>().FullFreeze(false);
        player.GetComponent<Player>().PauseMovement(1f);
        //player.GetComponent<Player>().ShowVisual(true);

        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetScreenSide() * pushForce * spawnOffset , 0), ForceMode2D.Impulse);

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
