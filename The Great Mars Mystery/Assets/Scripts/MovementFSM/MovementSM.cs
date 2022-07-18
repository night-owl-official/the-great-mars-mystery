using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving movingState;

    [Header("Player Rigidbody")]
    public Rigidbody2D rb;

    [Header("Player Transform")]
    public Transform tf;
    
    [Header("Camera Transform")]
    public Transform ctf;

    public float speed = 4f;
    

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
      

    }
    
    protected override BaseState GetInitialState()
    {
        return idleState;
    }

}
