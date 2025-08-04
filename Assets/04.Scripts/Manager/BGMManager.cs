using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm1;
    public AudioClip bgm2;

    public void PlayBGM(int index)
    {
        AudioClip selectedClip = index == 1 ? bgm1 : bgm2;

        audioSource.clip = selectedClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}

