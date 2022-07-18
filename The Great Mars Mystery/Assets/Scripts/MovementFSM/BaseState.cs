﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void enter()
    {

    }
    
    public virtual void updateLogic()
    {

    }

    public virtual void updatePhysics()
    {

    }
    
    public virtual void exit()
    {

    }
}
