using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;
    public bool playOnAwake =false;
    public bool loop;


    [HideInInspector]
    public AudioSource source;
}
