using UnityEngine;

public class InteractionObject : MonoBehaviour {
    #region Member variables
    [SerializeField]
    private Transform carExitPoint;

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
    #endregion
}
