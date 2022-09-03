using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour {
    #region Member variables
    private bool playerDetected;

    [SerializeField]
    private string sceneThisDoorLeadsTo;
    #endregion

    #region Methods
    // Update is called once per frame
    private void Update() {
        if (playerDetected && Input.GetButtonDown("Interact")) {
            if (SceneManager.GetActiveScene().name == "external_map") {
                Vector2 currentPlayerPos = GameObject.Find("Player(Clone)").transform.position;
                Vector2 currentCarPos = GameObject.Find("Car(Clone)").transform.position;
                FindObjectOfType<GameManager>().SetPlayerPosition(currentPlayerPos);
                FindObjectOfType<GameManager>().SetCarPosition(currentCarPos);
            }

            SceneManager.LoadScene(sceneThisDoorLeadsTo);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            playerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
            playerDetected = false;
    }
    #endregion
}
