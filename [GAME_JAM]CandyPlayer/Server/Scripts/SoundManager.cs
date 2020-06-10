using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : CSingleton<SoundManager>
{
    private float soundEffectVolume = 1.0f;
    public float SoundEffectVolume
    {
        get { return soundEffectVolume; }
        set
        {
            soundEffectVolume = value;

            for (int i = soundEffectList.Count - 1; i >= 0; i--)
                soundEffectList[i].ReleaseSound(soundEffectMute, soundEffectVolume);
        }
    }
    private float backgroundVolume = 1.0f;
    public float BackgroundVolume
    {
        get { return backgroundVolume; }
        set
        {
            backgroundVolume = value;

            for (int i = backgroundList.Count - 1; i >= 0; i--)
                backgroundList[i].ReleaseSound(backgroundMute, backgroundVolume);
        }
    }
    private float environmentVolume = 1.0f;
    public float EnvironmentVolume
    {
        get { return environmentVolume; }
        set
        {
            environmentVolume = value;

            for (int i = EnvironmentList.Count - 1; i >= 0; i--)
                EnvironmentList[i].ReleaseSound(environmentMute, environmentVolume);
        }
    }
    
    private bool soundEffectMute = false;
    public bool SoundEffectMute
    {
        get { return soundEffectMute; }
        set
        {
            soundEffectMute = value;

            for (int i = soundEffectList.Count - 1; i >= 0; i--)
                soundEffectList[i].ReleaseSound(soundEffectMute, soundEffectVolume);
        }
    }
    
    private bool backgroundMute = false;
    public bool BackgroundMute
    {
        get { return backgroundMute; }
        set
        {
            backgroundMute = value;

            for (int i = backgroundList.Count - 1; i >= 0; i--)
                backgroundList[i].ReleaseSound(backgroundMute, backgroundVolume);
        }
    }
    private bool environmentMute = false;
    public bool EnvironmentMute
    {
        get { return environmentMute; }
        set
        {
            environmentMute = value;

            for (int i = EnvironmentList.Count - 1; i >= 0; i--)
                EnvironmentList[i].ReleaseSound(environmentMute, environmentVolume);
        }
    }

    [HideInInspector]
    public List<SoundPlayer> soundEffectList;
    [HideInInspector]
    public List<SoundPlayer> backgroundList;
    [HideInInspector]
    public List<SoundPlayer> EnvironmentList;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SceneMove()
    {
        soundEffectList.Clear();
        backgroundList.Clear();
        EnvironmentList.Clear();
    }
}
