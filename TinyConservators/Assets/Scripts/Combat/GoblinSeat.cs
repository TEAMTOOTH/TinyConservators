using UnityEngine;

public class GoblinSeat : MonoBehaviour
{
    [SerializeField] GameObject ridingGoblin;
    public void KnockedOff()
    {
        GameObject minion = Instantiate(ridingGoblin, transform.position, Quaternion.identity);
        Enemy e = GetComponentInParent<Enemy>();


        minion.GetComponent<Minion>().SetMount(e);
        e.SetMinion(minion);


    }
}
