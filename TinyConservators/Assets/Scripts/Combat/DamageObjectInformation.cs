using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DamageObjectInformation")]
public class DamageObjectInformation : ScriptableObject
{
    public GameObject damageObject;
    public int amount;
    public float revolutionTime;
    public float size;
}
