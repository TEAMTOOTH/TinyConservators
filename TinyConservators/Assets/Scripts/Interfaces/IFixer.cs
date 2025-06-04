using UnityEngine;

public interface IFixer
{
    public void SetOwner(GameObject objectToFix);
    public void Fix();
}
