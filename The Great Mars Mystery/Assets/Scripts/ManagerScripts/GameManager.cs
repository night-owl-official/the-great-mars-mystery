using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Member variables
    [SerializeField]
    private GameObject playerPrefrab;

    [SerializeField]
    private GameObject carPrefab;

    private static List<string> characterNamesAlreadySpokenTo = new List<string>();

    private Vector3 playerPos;
    private Vector3 carPos;
    #endregion

    #region Methods
    public static void AddCharacterNameToAlreadySpokenToList(string charName) {
        if (characterNamesAlreadySpokenTo.Contains(charName))
            return;

        characterNamesAlreadySpokenTo.Add(charName);
    }

    public static bool IsCharacterNameInAlreadySpokenToList(string charName) {
        return characterNamesAlreadySpokenTo.Contains(charName);
    }

    public void StartGame() {
        playerPos = new Vector3(0, 0, 0);
        carPos = new Vector3(3, 0, 0);

        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        playerPos = new Vector3(0, 0, 0);
        carPos  = new Vector3(3, 0, 0);

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        string currentName = current.name;

        if (currentName == null)
            // Scene1 has been removed
            currentName = "Replaced";

        if (next.name == "external_map") {
            Instantiate(playerPrefrab, playerPos, Quaternion.identity);
            Instantiate(carPrefab, carPos, Quaternion.identity);
        }


    }
    public void killScreen() {
        Debug.Log("KILL SCREEN");
    }

    public void SetPlayerPosition(Vector2 pos) {
        playerPos = pos;
    }

    public void SetCarPosition(Vector2 pos) {
        carPos = pos;
    }
    #endregion
}
