using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]

public class Sound
{
    public string clipName;
    public AudioClip clip;
    public float volume;
    public float pitch;
    public bool loop;
}