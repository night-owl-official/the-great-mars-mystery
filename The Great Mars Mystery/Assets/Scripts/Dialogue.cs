using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //Fields
    //Window
    public GameObject window;
    //Indicator
    public GameObject indicator;
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public List<string> dialogues;
    //Writing speed
    public float writingSpeed;
    //Selected Dialogue index
    private int index;
    //Character index
    private int charIndex;
    //Started boolean
    private bool started;
    //Wait for next boolean
    private bool waitForNext;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if(started)
        {
            return;
        }

        //Boolean to indicate that we have started
        started = true;
        //Show the window
        ToggleWindow(true);
        //hide the indicator
        ToggleIndicator(false);
        //start with first dialogue
        GetDialogue(0);
        
    }

    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;
        //reset the character index
        charIndex = 0;
        //clear the dialogue component text
        dialogueText.text = string.Empty;
        //start writing
        StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        
        //Hide the window
        ToggleWindow(false);


        //Reset the dialogue to start
        StopAllCoroutines();
        started = false;
        waitForNext = false;
    }

    //Writing logic
    IEnumerator Writing()
    {
        //Wait x seconds
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character index
        charIndex++;
        //Make sure you have reached the end of the sentence
        if(charIndex < currentDialogue.Length)
        {
            //Wait x seconds
            yield return new WaitForSeconds(writingSpeed);
            //restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
        
    }

    private void Update()
    {
        if(!started)
        {
            return;
        }

        if(waitForNext && Input.GetButtonDown("Interact"))
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope of dialogues List
            if (index < dialogues.Count)
            {
                //If so fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                //If not End dialogue
                ToggleIndicator(true);
                EndDialogue();
            }
            
        }
    }
}
