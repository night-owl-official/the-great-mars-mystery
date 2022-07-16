using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform shootingPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit2D hitInfo= Physics2D.Raycast(shootingPoint.position, shootingPoint.right);

        if(hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
        }
    }
}
