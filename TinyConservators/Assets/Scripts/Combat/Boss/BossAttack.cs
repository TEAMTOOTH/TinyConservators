using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject[] attackSpots;
    [SerializeField] float maxEatTime;

    bool eating;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] GetAttackSpots()
    {
        return attackSpots;
    }

    public void Attack()
    {
        float time = 0;
        eating = true;
        StartCoroutine(Eat());
        //Spawn bubble and decal.
        IEnumerator Eat()
        {
            Debug.Log("Eating");
            
            while (eating)
            {
                time += Time.deltaTime;
                //Debug.Log(time);
                if(time > maxEatTime)
                {
                    Debug.Log("Heading off");
                    eating = false;
                    GetComponent<Boss>().State = BossStates.walkOff;
                    
                }
                yield return null;

                //Do damage


            }        
        } 
    }

    public void SetMaxEatingTime(float maxEatingTime)
    {
        maxEatTime = maxEatingTime;
    }
}
