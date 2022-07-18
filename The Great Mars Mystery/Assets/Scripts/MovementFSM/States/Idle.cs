using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private MovementSM _sm;
    private float _horizontalInput;
    private float _verticalInput;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) {
        _sm = (MovementSM)stateMachine;
    }

    public override void enter()
    {
        base.enter();
        
        _horizontalInput = 0f;
        _verticalInput = 0f;
}

    public override void updateLogic()
    {
        base.updateLogic();

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon && Mathf.Abs(_verticalInput) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.movingState);
        }
        if (Mathf.Abs(_verticalInput) > Mathf.Epsilon && Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.movingState);
        }
    }
    public override void updatePhysics()
    {
        base.updatePhysics();
        Vector2 vel = _sm.rb.velocity;

        vel.x = _horizontalInput * 0;
        vel.y = _verticalInput * 0;

        _sm.rb.velocity = vel;
    }
}
