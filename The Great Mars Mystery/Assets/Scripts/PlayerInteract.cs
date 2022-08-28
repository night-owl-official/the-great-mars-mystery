using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterObj = null;


    private void Update()
    {
        if(Input.GetButtonDown("Interact") && currentInterObj)
        {
            currentInterObj.SendMessage("DoInteraction");
            if (currentInterObj.name == "Car")
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Car"))
        {
            Debug.Log(other.name);
            currentInterObj = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
           if(other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
    }
}
