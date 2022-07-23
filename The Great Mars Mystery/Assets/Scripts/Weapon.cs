using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // The point in space where the ray is cast
    public Transform shootingPoint;
    public LineRenderer bulletEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine (Shoot());
        }
    }

    IEnumerator Shoot()
    {
        //Records information about the object it hit
        RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position,shootingPoint.up);

        //If anything is hit with the ray and it is not null
        if(hit)
        {
            Debug.Log(hit.transform.name);

            bulletEffect.SetPosition(0,shootingPoint.position);
            bulletEffect.SetPosition(1,hit.point);
        }
        else
        {
            bulletEffect.SetPosition(0,shootingPoint.position);
            bulletEffect.SetPosition(1,shootingPoint.position + shootingPoint.up * 100);
        }

        bulletEffect.enabled=true;

        yield return new WaitForSeconds(0.02f);

        bulletEffect.enabled=false;
    }
}
