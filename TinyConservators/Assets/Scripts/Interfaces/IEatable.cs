using UnityEngine;

public interface IEatable
{
    public void Eat(GameObject eater);

    public void SpitOut(GameObject spitter);

    public void Consumed(GameObject consumer);

    public bool Eatable();

    public void SetEatable(bool state);

    public bool Spittable();
}
