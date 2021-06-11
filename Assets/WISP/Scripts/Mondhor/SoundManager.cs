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

    private static Dictionary<SoundEnum, float> soundTimers;

    private static GameObject PlayOnceGameObject;
    private static AudioSource PlayOnceAudioSource;

    public static void InitializeTimers()
    {
        soundTimers = new Dictionary<SoundEnum, float>();
        soundTimers[SoundEnum.PlayerMove] = 0f;
    }

    //public static void PlaySound3d(SoundEnum sound, Vector3 position)
    //{
    //    if (CanPlaySound(sound))
    //    {
    //        GameObject soundGameObject = new GameObject("Sound");
    //        soundGameObject.transform.position = position;
    //        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
    //        audioSource.clip = GetAudioClip(sound);
    //        audioSource.Play();

    //        Object.Destroy(soundGameObject, audioSource.clip.length);
    //    }
    //}

    public static void PlaySound(SoundEnum sound)
    {
        if (CanPlaySound(sound))
        {
            if(PlayOnceGameObject == null)
            {
                PlayOnceGameObject = new GameObject("Play Once Sound");
                PlayOnceAudioSource = PlayOnceGameObject.AddComponent<AudioSource>();
            }
            else
            {
                PlayOnceAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }
    }

    private static bool CanPlaySound(SoundEnum sound)
    {
        switch (sound)
        {
            default:
                return true;
            case SoundEnum.PlayerMove:
                if (soundTimers.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimers[sound];
                    float playerMoveTimer = 0.33f;
                    if (lastTimePlayed + playerMoveTimer < Time.time)
                    {
                        soundTimers[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            //case SoundEnum.LanternBurning:
            //    if (soundTimers.ContainsKey(sound))
            //    {
            //        float lastTimePlayed = soundTimers[sound];
            //        float playerMoveTimer = 1.0f;
            //        if (lastTimePlayed + playerMoveTimer < Time.time)
            //        {
            //            soundTimers[sound] = Time.time;
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        return true;
            //    }
        }
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
