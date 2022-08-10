using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followCarTransform;
    public Transform followPlayerTransform;
    public GameObject player;
    public GameObject car;
    private void FixedUpdate()
    {
        if (player.activeSelf)
        {
            this.transform.position = new Vector3(followPlayerTransform.position.x, followPlayerTransform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(followCarTransform.position.x, followCarTransform.position.y, this.transform.position.z);
        }
    }
}
