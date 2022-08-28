using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefrab;

    public Vector3 playerPos;

    void Start()
    {
        playerPos =new Vector3(0,0,0);

         SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        if(next.name == "external_map")
        {
            Instantiate(playerPrefrab,playerPos,Quaternion.identity);
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }
    public void killScreen()
    {
        Debug.Log("KILL SCREEN");
    }


    
}
