using UnityEngine;

public class GoblinSeat : MonoBehaviour
{
    [SerializeField]GameObject ridingGoblin;
    public void KnockedOff()
    {
        Instantiate(ridingGoblin, transform.position, Quaternion.identity);
    }
}
