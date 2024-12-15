using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    //public float mStamina=10f;
    //public float mSpeed = 10f;
    protected StateMachine StateMachine;

    protected virtual void Awake()
    {
        StateMachine = new StateMachine();
    }

    public virtual void Update()
    {
        StateMachine.UpdateState();
    }
}
