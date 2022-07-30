//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DetectPlayer : MonoBehaviour
//{
//    public Rigidbody2D rb;
//    public GameObject player;
//    public float detectionRadius;
//    public float escapeRadius;
//    public bool isChasing;
//    public float rotationAdjustment;
//    public float maintainRelativeDist;

//    [HideInInspector]
//    public Vector3 direction;

//    [HideInInspector]
//    public float angle;

//    [HideInInspector]
//    public float dist;

//    AIPatrol aiPatrol;
//    // Start is called before the first frame update
//    void Start()
//    {
//        isChasing = false;

//        aiPatrol = this.GetComponent<AIPatrol>();
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        dist = Vector3.Distance(player.transform.position, rb.transform.position);

//        //Debug.Log(isChasing);
//        if (dist < detectionRadius)
//        {
//            isChasing = true;
//            aiPatrol.m_isPatrolling = false;
//        }

//        if (dist > escapeRadius)
//        {
//            isChasing = false;
//            aiPatrol.m_isPatrolling = true;
//        }

//        if (isChasing)
//        {
//            direction = player.transform.position - rb.transform.position;
//            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationAdjustment;
//            direction.Normalize();
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (isChasing) {
            
//            if (dist > maintainRelativeDist)
//            {
//                rb.MovePosition(rb.transform.position + (direction * aiPatrol.m_walkSpeed * Time.deltaTime));
//            }
            
//            rb.MoveRotation(angle);
//        }
//    }
//}

