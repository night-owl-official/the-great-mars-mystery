using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    #region Member variables
    private GameObject currentInterObj = null;
    #endregion

    #region Methods
    private void Update() {
        if (Input.GetButtonDown("Interact") && currentInterObj) {
            currentInterObj.SendMessage("DoInteraction");

            if (currentInterObj.name == "Car(Clone)")
                gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Car"))
            currentInterObj = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Car"))
            if (other.gameObject == currentInterObj)
                currentInterObj = null;
    }
    #endregion
}
