using UnityEngine;

/*Attaches to the main camera and updates it transform to be the same as the players
 will track the car instead if the player gameObject is deactivated*/
public class CameraFollow : MonoBehaviour
{
    #region Methods
    private void Start()
    {
        m_playerTransform = player.transform;
        m_carTransform = car.transform;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (player.activeSelf)
            {
                //updates the camera transform to the players changed position
                this.transform.position = new Vector3(m_playerTransform.position.x, m_playerTransform.position.y, this.transform.position.z);
            }
            else if (car !=null)
            {
                //updates the camera transform to the cars changed position only if the player gameobject has been deactivated
                this.transform.position = new Vector3(m_carTransform.position.x, m_carTransform.position.y, this.transform.position.z);
            }
        }
    }
    #endregion

    #region Member variables
    public GameObject player;
    public GameObject car;
    private Transform m_playerTransform;
    private Transform m_carTransform;
    #endregion
}
