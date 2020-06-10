using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
enum ESoundType
{
    SoundEffect,
    Background,
    Environment
}

[SerializeField]
enum EAudioType
{
    Sound2D,
    Sound3D
}

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private ESoundType soundType;
    [SerializeField]
    private EAudioType audioType;
    [SerializeField]
    private float distance;

    private void Start()
    {
        /*
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;

        switch (soundType)
        {
            case ESoundType.SoundEffect:
                audioSource.playOnAwake = false;
                audioSource.loop = false;

                SoundManager.Instance.soundEffectList.Add(this);

                audioSource.mute = SoundManager.Instance.SoundEffectMute;
                audioSource.volume = SoundManager.Instance.SoundEffectVolume;
                break;

            case ESoundType.Background:
                audioSource.playOnAwake = false;
                audioSource.loop = true;

                SoundManager.Instance.backgroundList.Add(this);

                audioSource.mute = SoundManager.Instance.BackgroundMute;
                audioSource.volume = SoundManager.Instance.BackgroundVolume;

                audioSource.Play();
                break;
            case ESoundType.Environment:
                audioSource.playOnAwake = false;
                audioSource.loop = true;

                SoundManager.Instance.EnvironmentList.Add(this);

                audioSource.mute = SoundManager.Instance.EnvironmentMute;
                audioSource.volume = SoundManager.Instance.EnvironmentVolume;

                audioSource.Play();
                break;
        }

        switch (audioType)
        {
            case EAudioType.Sound2D:
                audioSource.spatialBlend = 0.0f;
                break;

            case EAudioType.Sound3D:
                audioSource.spatialBlend = 1.0f;
                audioSource.maxDistance = distance;
                audioSource.minDistance = 0.0f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                break;
        }*/
    }

    public void ReleaseSound(bool _mute, float _volume)
    {
        audioSource.mute = _mute;
        audioSource.volume = _volume * 0.1f;
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
