using System;
using System.Collections;
using System.Collections.Generic;
using Autodesk.Fbx;
using Nidavellir.Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir.Audio
{
    /// <summary>
    /// This component serves as a music player. You can give it any title theme which is played whenever you are in the Main Menu Scene and a Game Theme.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicData m_titleTheme;
        [SerializeField] private MusicData m_gameTheme;
        
        private List<AudioSource> m_audioSources;
        private MusicData m_currentMusicData;

        private int m_audioSourceToggle = 0;
        private double m_nextStartTime;
        private double m_latestQueueTime;

        private static MusicPlayer s_instance;

        public static MusicPlayer Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<MusicPlayer>();
                }

                return s_instance;
            }
        }

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
            
            this.m_audioSources = new();
            this.m_audioSources.Add(this.AddComponent<AudioSource>());
            this.m_audioSources.Add(this.AddComponent<AudioSource>());
            
            foreach (var audioSource in this.m_audioSources)
            {
                audioSource.playOnAwake = false;
            }
            
            GlobalSettings.Instance.MusicVolumeChanged += this.OnMusicVolumeChanged;
            SceneManager.sceneLoaded += this.SceneChanged;
        }

        private void Update()
        {
            if (this.m_currentMusicData == null)
                return;
            
            if (AudioSettings.dspTime > this.m_nextStartTime - 1)
            {
                if(this.m_currentMusicData.FollowingClip != null)
                    this.m_currentMusicData = this.m_currentMusicData.FollowingClip;
                else if (!this.m_currentMusicData.Looping)
                    this.m_currentMusicData = null;

                this.m_audioSourceToggle = 1 - this.m_audioSourceToggle;
                var nextAudioSource = this.m_audioSources[this.m_audioSourceToggle];
                this.PlayScheduledClip(this.m_currentMusicData, nextAudioSource, this.m_nextStartTime);
                var duration = (double) nextAudioSource.clip.samples / nextAudioSource.clip.frequency;
                this.m_nextStartTime += duration;
            }
        }

        private void OnMusicVolumeChanged(object sender, System.EventArgs e)
        {
            this.m_audioSources[this.m_audioSourceToggle].volume = this.m_currentMusicData.Volume * GlobalSettings.Instance.MusicVolume;
        }

        private void PlayClip(MusicData toPlay, AudioSource audioSource, bool looping)
        {
            audioSource.clip = toPlay.MusicClip;
            audioSource.volume = toPlay.Volume * GlobalSettings.Instance.MusicVolume;
            audioSource.loop = looping;
            audioSource.Play();
            
        }        
        
        private void PlayScheduledClip(MusicData toPlay, AudioSource audioSource, double startTime)
        {
            audioSource.clip = toPlay.MusicClip;
            audioSource.volume = toPlay.Volume * GlobalSettings.Instance.MusicVolume;
            audioSource.PlayScheduled(startTime);
        }


        private void PlayClipList(MusicData toPlay)
        {
            if (this.m_currentMusicData != null)
            {
                this.m_audioSources[this.m_audioSourceToggle].Stop();
            }
            
            this.m_currentMusicData = toPlay;

            if (this.m_currentMusicData.MusicClip == null)
            {
                Debug.LogError($"MusicData {toPlay.name} has no {nameof(toPlay.MusicClip)}.");
                return;
            }

            this.m_nextStartTime = AudioSettings.dspTime + 0.2f;
            this.m_audioSourceToggle = 1 - this.m_audioSourceToggle;
            var nextAudioSource = this.m_audioSources[this.m_audioSourceToggle];
            this.PlayScheduledClip(this.m_currentMusicData, nextAudioSource, this.m_nextStartTime);
            var duration = (double) nextAudioSource.clip.samples / nextAudioSource.clip.frequency;
            this.m_nextStartTime += duration;
        }

        private void SceneChanged(Scene loadedScene, LoadSceneMode arg1)
        {
            var hasLoadedMainMenu = loadedScene.buildIndex == 0;

            if (hasLoadedMainMenu)
                this.PlayClipList(this.m_titleTheme);
            else if (loadedScene.buildIndex == 1)
                this.PlayClipList(this.m_gameTheme);
        }
    }
}