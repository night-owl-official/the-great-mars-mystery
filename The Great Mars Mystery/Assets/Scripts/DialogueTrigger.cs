using UnityEngine;


public class DialogueTrigger : MonoBehaviour {
    #region Methods
    private void Start() {
        dialogueScript = GetComponentInChildren<Dialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }

    private void Update() {
        if (playerDetected && Input.GetButtonDown("Interact"))
            dialogueScript.StartDialogue();
    }
    #endregion

    #region Member variables
    private Dialogue dialogueScript;
    private bool playerDetected;
    #endregion
}
