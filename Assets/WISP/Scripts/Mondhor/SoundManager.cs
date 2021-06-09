using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum SoundEnum
    {
        PlayerMove,
        PlayerFlash,
        LanternBurning,
        LanternTrigger,
        BatOpeningEyes,
        BatCharging,

    }

    public static void PlaySound(SoundEnum sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        //soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        //audioSource.clip = GetAudioClip(sound);
        audioSource.PlayOneShot(GetAudioClip(sound));
        
    }

    private static AudioClip GetAudioClip(SoundEnum sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Son " + sound + " introuvable");
        return null;
    }

}
