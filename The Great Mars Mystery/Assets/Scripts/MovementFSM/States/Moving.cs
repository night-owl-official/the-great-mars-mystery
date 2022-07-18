using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private MovementSM _sm;
    private float _horizontalInput;
    private float _verticalInput;
    private float look;

    public Moving(MovementSM stateMachine) : base("Moving", stateMachine) {
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

        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon && Mathf.Abs(_verticalInput) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.idleState);
        }
        if (Mathf.Abs(_verticalInput) > Mathf.Epsilon && Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.idleState);
        }
    }
    
    public override void updatePhysics()
    {
        base.updatePhysics();
        Vector2 vel = _sm.rb.velocity;
        
        //controls the rotation of the player sprite changing to the direction the sprite is looking in. 
        if (_horizontalInput > 0 || _horizontalInput < 0)
        {
            look = (90 * _horizontalInput)* -1;
        }
        
        if (_verticalInput > 0 || _verticalInput < 0)
        {
            look = 90 + ((90 * _verticalInput) * -1);
        }

        //sets the new speed of the player in the direction of travel
        vel.x = _horizontalInput * _sm.speed;
        vel.y = _verticalInput * _sm.speed;

        //updates the player look direction and velocity
        _sm.tf.eulerAngles = new Vector3(0,0,look);
        _sm.rb.velocity = vel;

        _sm.ctf.position = new Vector3(_sm.tf.position.x, _sm.tf.position.y, _sm.ctf.position.z);
    }

}
