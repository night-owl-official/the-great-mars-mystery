using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.enter();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.updateLogic();
        }
    }

    void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.updatePhysics();
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.exit();

        currentState = newState;

        currentState.enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    //Prints the current state on the screen for debugging purposes
    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='white'><size=40>{content}</size></color>");
    }
}

