using UnityEngine;

/*Attaches to the main camera and updates its transform to be the same as the players
 will track the car instead if the player gameObject is deactivated*/
public class CameraFollow : MonoBehaviour {
    #region Methods
    private void Start() {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        carTransform = GameObject.FindWithTag("Car")?.transform;
    }

    private void LateUpdate() {
        if (!playerTransform)
            return;

        if (playerTransform.gameObject.activeSelf)
            LerpCurrentPositionToTargetPosition(playerTransform.position);
        else if (carTransform)
            LerpCurrentPositionToTargetPosition(carTransform.position);
    }

    private void LerpCurrentPositionToTargetPosition(Vector3 targetPosition) {
        transform.position =
            new Vector3(
                Mathf.Lerp(transform.position.x, targetPosition.x, cameraSmoothing * Time.deltaTime),
                Mathf.Lerp(transform.position.y, targetPosition.y, cameraSmoothing * Time.deltaTime),
                transform.position.z
            );
    }
    #endregion

    #region Member variables
    [SerializeField]
    [Range(0.1f, 10f)]
    private float cameraSmoothing = 1.5f;

    private Transform playerTransform;
    private Transform carTransform;
    #endregion
}
