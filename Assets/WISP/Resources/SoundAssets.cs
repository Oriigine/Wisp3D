using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public SoundAudioClip[] soundAudioClipArray;

    private static SoundAssets _i;
    private void Awake()
    {
        if (_i == null || _i == this)
            _i = this;
        else
            Destroy(this);
    }

    public static SoundAssets i
    {
        get 
        {
            if (_i == null)
            {
                _i = FindObjectOfType<SoundAssets>();
            }
            if(_i == null)
            {
                GameObject obj = new GameObject("Sound Assets");
                _i = obj.AddComponent<SoundAssets>();
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
