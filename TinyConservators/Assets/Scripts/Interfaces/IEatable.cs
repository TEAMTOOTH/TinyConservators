using UnityEngine;

public interface IEatable
{
    public void Eat(GameObject eater);

    public void SpitOut(GameObject spitter);
}
