using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //This calculated the angle to rotate the character by in order to follow the mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos- transform.position;
        float rotZ = Mathf.Atan2(rotation.y,rotation.x)* Mathf.Rad2Deg-90f;
        transform.rotation=Quaternion.Euler(0,0,rotZ);
    }
}
