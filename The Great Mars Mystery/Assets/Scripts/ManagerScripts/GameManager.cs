using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Member variables
    [SerializeField]
    private GameObject playerPrefrab;

    private Vector3 playerPos;
    #endregion

    #region Methods
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        playerPos = new Vector3(0, 0, 0);

        SceneManager.LoadScene(1);
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        string currentName = current.name;

        if (currentName == null)
            // Scene1 has been removed
            currentName = "Replaced";

        if (next.name == "external_map")
            Instantiate(playerPrefrab, playerPos, Quaternion.identity);
    }
    public void killScreen() {
        Debug.Log("KILL SCREEN");
    }

    public void SetPlayerPosition(Vector2 pos) {
        playerPos = pos;
    }
    #endregion
}
