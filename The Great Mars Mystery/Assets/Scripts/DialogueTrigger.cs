using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    #region Methods
    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we triggered the player enable player detected and show indicator
        if(collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we triggered the player enable player detected and show indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }

    //While detected if we interact start the dialogue
    private void Update()
    {
        if(playerDetected && Input.GetButtonDown("Interact"))
        {
            dialogueScript.StartDialogue();
        }
    }
    #endregion

    #region Member variables
    public Dialogue dialogueScript;
    private bool playerDetected;
    #endregion
}
