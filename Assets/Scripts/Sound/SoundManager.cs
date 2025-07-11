using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sfx;
    [SerializeField] private Sound[] bgm;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public static SoundManager instance;

    [SerializeField] private float masterMult;
    [SerializeField] private float musicVol;
    [SerializeField] private float sfxVol;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            masterMult = 1.0f;
            musicVol = 1.0f;
            sfxVol = 1.0f;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgm, x => x.clipName == name);
        if (s == null)
        {
            Debug.Log("No file");
            return;
        }
        else
        {
            bgmSource.clip = s.clip;
            bgmSource.loop = s.loop;
            bgmSource.pitch = s.pitch;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, x => x.clipName == name);
        if (s == null)
        {
            Debug.Log("No file");
            return;
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.pitch = s.pitch;
            sfxSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopSfx()
    {
        sfxSource.Stop();
    }
    public void calMusicVol(float musicVal)
    {
        musicVol = musicVal;
        setMusicVol();
    }

    public void calSfxVol(float sfxVal)
    {
        sfxVol = sfxVal;
        setSfxVol();
    }

    public void calMaster(float masterVal)
    {
        masterMult = masterVal;
        setMusicVol();
        setSfxVol();
    }

    private void setMusicVol()
    {
        bgmSource.volume = musicVol * masterMult;
    }

    private void setSfxVol()
    {
        sfxSource.volume = sfxVol * masterMult;
    }

    private void mute()
    {
        calMaster(0.0f);
    }

    private void unmute()
    {
        calMaster(masterMult);
    }
}
