using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public SoundAudioClip[] soundAudioClipArray;

    private static SoundAssets _i;

    public static SoundAssets i
    {
        get 
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            }
            return _i;
            
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.SoundEnum sound;
        public AudioClip audioClip;
    }

}
