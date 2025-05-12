using UnityEngine;

public class GoblinSeat : MonoBehaviour
{
    [SerializeField]GameObject ridingGoblin;
    public void KnockedOff()
    {
        GameObject minion = Instantiate(ridingGoblin, transform.position, Quaternion.identity);
        minion.GetComponent<Minion>().SetMount(GetComponentInParent<Enemy>());


    }
}
