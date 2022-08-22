using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {
    #region Methods
    private void Awake() {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show) {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show) {
        indicator.SetActive(show);
    }
    
    public void StartDialogue() {
        if (started)
            return;

        started = true;
        ToggleWindow(true);
        ToggleIndicator(false);

        //start with first dialogue
        GetDialogue(0);
    }

    private void GetDialogue(int i) {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;
        
        StartCoroutine(Writing());
    }

    public void EndDialogue() {
        ToggleWindow(false);

        //Reset the dialogue to start
        StopAllCoroutines();
        started = false;
        waitForNext = false;
    }

    //Writing logic
    //Writes the text like a typewriter one letter at a time
    private IEnumerator Writing() {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        
        dialogueText.text += currentDialogue[charIndex++];
        
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length) {
            yield return new WaitForSeconds(writingSpeed);
            //restart the same process
            StartCoroutine(Writing());
        } else {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
    }

    private void Update() {
        if (!started)
            return;

        if (waitForNext && Input.GetButtonDown("Interact")) {
            waitForNext = false;
            index++;

            //Check if we are in the scope of dialogues List
            if (index < dialogues.Count) {
                //If so fetch the next dialogue
                GetDialogue(index);
            } else {
                //If not End dialogue
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameObject window;

    [SerializeField]
    private GameObject indicator;

    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private List<string> dialogues;

    [SerializeField]
    private float writingSpeed;

    //Selected Dialogue index
    private int index;
    private int charIndex;
    private bool started;
    private bool waitForNext;
    #endregion
}
