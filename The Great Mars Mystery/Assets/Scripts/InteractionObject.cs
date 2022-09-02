using UnityEngine;

public class InteractionObject : MonoBehaviour {
    #region Member variables
    private GameObject player;
    #endregion

    #region Methods
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && gameObject.GetComponent<CarMovement>().enabled) {
            player.SetActive(true);
            player.transform.position =
                new Vector3(transform.position.x + 1, transform.position.y, transform.position.z - 1f);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CarMovement>().enabled = false;
        }

        Debug.Log(player);
    }

    public void DoInteraction() {
        if (!gameObject.GetComponent<CarMovement>().enabled) {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CarMovement>().enabled = true;
        }
    }
    #endregion
}
