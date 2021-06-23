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
        BackgroundMusic,
        DoorOpening,
        LightDetector
        
    }


    private static Vector3 gameObjectPosition;
    private static Dictionary<SoundEnum, float> soundTimers;

    private static GameObject PlayOnceGameObject;
    private static AudioSource PlayOnceAudioSource;

    public static void InitializeTimers()
    {
        soundTimers = new Dictionary<SoundEnum, float>();
        soundTimers[SoundEnum.PlayerMove] = 0f;
    }

    public static void PlaySound3d(SoundEnum sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject3D = new GameObject("3D Sound : " + sound);
            soundGameObject3D.transform.position = position;
            AudioSource audioSource = soundGameObject3D.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();       

            //Object.Destroy(soundGameObject3D, audioSource.clip.length);
        }
        
    }

    public static void PlaySound(SoundEnum sound)
    {
        if (CanPlaySound(sound))
        {
            if(PlayOnceGameObject == null)
            {
                PlayOnceGameObject = new GameObject("Play Once Sound : " + sound);
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
            case SoundEnum.BackgroundMusic:
                if (soundTimers.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimers[sound];
                    float playerMoveTimer = 97.545f;
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
