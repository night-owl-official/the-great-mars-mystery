using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour {
    #region Member variables
    private bool playerDetected;

    [SerializeField]
    private string sceneThisDoorLeadsTo;

    [SerializeField]
    private GameObject indicator;
    #endregion

    #region Methods
    private void Awake() {
        indicator.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        if (playerDetected && Input.GetButtonDown("Interact")) {
            if (SceneManager.GetActiveScene().name == "external_map" ||
                SceneManager.GetActiveScene().name == "external_map_2") {
                Vector2 currentPlayerPos = GameObject.FindWithTag("Player").transform.position;
                Vector2 currentCarPosition = GameObject.FindWithTag("Car").transform.position;
                FindObjectOfType<GameManager>().SetPlayerPosition(currentPlayerPos);
                FindObjectOfType<GameManager>().SetCarPosition(currentCarPosition);
            }

            
            SceneManager.LoadScene(sceneThisDoorLeadsTo);
            FindObjectOfType<SoundManager>().Play("door_unlock");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !GameManager.IsFinalBossAlive) {
            playerDetected = true;
            indicator.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            playerDetected = false;
            indicator.SetActive(false);
        }

    }
    #endregion
}
