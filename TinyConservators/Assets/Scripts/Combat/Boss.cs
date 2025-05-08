using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    BossStates state;
    public BossStates State
    {
        get => state;
        set
        {
            if (state != value)
            {
                BossStates oldState = state;
                state = value;
                OnBossStateChanged?.Invoke(oldState, state);
            }
        }
    }

    public event Action<BossStates, BossStates> OnBossStateChanged;

    void Start()
    {
        OnBossStateChanged += (from, to) => StateChanged(from, to);
    }

    void StateChanged(BossStates from, BossStates to)
    {
        switch (to)
        {
            case BossStates.readying:
                break;
            case BossStates.movement:
                break;
            case BossStates.eating:
                break;
            case BossStates.hurt:
                break;
            default:
                break;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum BossStates
{
    readying,
    movement,
    eating,
    hurt
}
