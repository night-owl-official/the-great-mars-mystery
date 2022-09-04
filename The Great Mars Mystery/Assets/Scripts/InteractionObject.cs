using UnityEngine;

public class InteractionObject : MonoBehaviour {
    #region Member variables
    [SerializeField]
    private Transform carExitPoint;

    [SerializeField]
    private GameObject indicator;

    private GameObject player;
    #endregion

    #region Methods
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && gameObject.GetComponent<CarMovement>().enabled) {
            player.SetActive(true);
            player.transform.position = carExitPoint.position;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CarMovement>().enabled = false;
        }
    }

    public void DoInteraction() {
        if (!gameObject.GetComponent<CarMovement>().enabled) {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CarMovement>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            indicator.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
            indicator.SetActive(false);
    }
    #endregion
}
