using UnityEngine;

public interface IKnockoutable
{
    
    public void Knockout();

    public void Recover();

    public void PauseKnockout(float pauseTime);

    public bool IsKnockedOut();
    

    
}
