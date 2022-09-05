using UnityEngine;

public class CarInteraction : MonoBehaviour {
    #region Member variables
    [SerializeField]
    private Transform carExitPoint;

    [SerializeField]
    private GameObject indicator;

    private GameObject player;
    private bool isPlayerInRange = false;
    #endregion

    #region Methods
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        if (CanEnterCar() && isPlayerInRange)
            EnterCar();
        else if (CanExitCar())
            ExitCar();
    }

    private bool CanEnterCar() {
        return Input.GetButtonDown("Interact") && !gameObject.GetComponent<CarMovement>().enabled;
    }

    private void EnterCar() {
        player.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<CarMovement>().enabled = true;
    }

    private bool CanExitCar() {
        return Input.GetButtonDown("Interact") && gameObject.GetComponent<CarMovement>().enabled;
    }

    private void ExitCar() {
        player.SetActive(true);
        player.transform.position = carExitPoint.position;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<CarMovement>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            isPlayerInRange = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            isPlayerInRange = false;
            indicator.SetActive(false);
        }
    }
    #endregion
}
