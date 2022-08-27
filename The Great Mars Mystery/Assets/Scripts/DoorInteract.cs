using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour
{
    private bool playerDetected;
    private SceneLoader load_scene;

    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetButtonDown("Interact"))
            SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            playerDetected = false;
        }
    }
}
