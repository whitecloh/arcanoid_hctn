using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Managers
{
    [Serializable]
    public class SoundClipData
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }

    public enum SoundType
    {
        Hit,
        BlockDestroy,
        DamageHit,
        LostLevel,
        WinLevel,
        ButtonClick,
        ShowPanel,
        AddEffect
    }
    
    public class SoundManager : MonoBehaviour
    {
        [Header("Audio Clips")]
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private List<SoundClipData> soundClipDataList;

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;
        
        private static SoundManager instance;
        public static SoundManager Instance => instance;
        
        private void Awake()
        {
            if (instance == null) 
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } 
            else
            {
                Destroy(gameObject);
            }
            
            musicSource.loop = true;
            PlayBackgroundMusic();
        }

        private void PlayBackgroundMusic()
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        
        public void PlaySound(SoundType type)
        {
            var clip = GetSoundClip(type);
            if (clip != null)
            {
                soundSource.PlayOneShot(clip);
            }
        }
        
        private AudioClip GetSoundClip(SoundType type)
        {
            var soundClipData = soundClipDataList.Find(data => data.soundType == type);
            return soundClipData?.audioClip;
        }
    }
}
