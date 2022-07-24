using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    // Start is called before the first frame update

    public float walkSpeed;
    public bool isPatrolling;
    public Rigidbody2D rb;
    public GameObject[] targets;
    public int currentTarget;

    [HideInInspector]
    public Vector3 direction;

    [HideInInspector]
    public float angle;

    public float rotationAdjustment;

    void Start()
    {
        isPatrolling = true;
        currentTarget = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (isPatrolling)
        {
            rb.MovePosition(rb.transform.position + (direction * walkSpeed * Time.deltaTime));
            rb.MoveRotation(angle);
        }
    }

    void Patrol()
    {

        direction = targets[currentTarget].transform.position - rb.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationAdjustment;
        direction.Normalize();
        float dist = Vector3.Distance(targets[currentTarget].transform.position, rb.transform.position);
        if (dist < 0.1)
        {
            Debug.Log(currentTarget);
            currentTarget = currentTarget + 1;
            if (currentTarget == targets.Length)
            {
                currentTarget = 0;
            }
        }
    }
        
}


