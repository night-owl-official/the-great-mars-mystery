﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Member variables
    [SerializeField]
    private GameObject playerPrefrab;

    [SerializeField]
    private GameObject carPrefab;

    private static List<string> characterNamesAlreadySpokenTo = new List<string>();
    public static bool IsFinalBossAlive { get; set; }

    private Vector3 playerPos;
    private Transform carTransform;
    private SoundManager soundManager;
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
        //carTransform.position = new Vector3(3f, 0, 0);
        //carTransform.rotation = Quaternion.identity;
        PlayerHealth.Reset();

        IsFinalBossAlive = false;

        SceneManager.LoadScene(1);

        if (!soundManager)
            soundManager = FindObjectOfType<SoundManager>();

        soundManager.StopPlayingSound("game_over");
        soundManager.StopPlayingSound("Theme");
        soundManager.Play("tune");
    }

    public void GoBackToMainMenu() {
        IsFinalBossAlive = false;
        SceneManager.LoadScene(0);
        Destroy(gameObject); // Get rid of this game manager as there's already one in main menu
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start() {
        playerPos = new Vector3(0, 0, 0);
        //carTransform.position = new Vector3(3f, 0, 0);
        //carTransform.rotation = Quaternion.identity;

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        if (next.name == "apartment")
            IsFinalBossAlive = true;
    }

    public void killScreen() {
        SceneManager.LoadScene(5);

        if (!soundManager)
            soundManager = FindObjectOfType<SoundManager>();

        soundManager.StopPlayingSound("tune");
        soundManager.Play("game_over");

        IsFinalBossAlive = false;
    }

    public void SetPlayerPosition(Vector2 pos) {
        playerPos = pos;
    }

    public void SetCarTrasform(Transform carTransform) {
        this.carTransform = carTransform;
    }
    #endregion
}
