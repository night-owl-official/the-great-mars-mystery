using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    GameObject ThePlayer;

    private void Start()
    {
        ThePlayer = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && gameObject.GetComponent<CarMovement>().enabled)
        {
            Debug.Log(ThePlayer.name);
            ThePlayer.SetActive(true);
            ThePlayer.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z-1);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CarMovement>().enabled = false;
        }
    }
    public void DoInteraction()
    {
        if (!gameObject.GetComponent<CarMovement>().enabled)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CarMovement>().enabled = true;
        }
    }

}
