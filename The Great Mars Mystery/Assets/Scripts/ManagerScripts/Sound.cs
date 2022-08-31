using UnityEngine;

[System.Serializable]
public class Sound {
    public AudioClip clip;

    public string name;
    public bool playOnAwake = false;
    public bool loop;


    [HideInInspector]
    public AudioSource source;
}
