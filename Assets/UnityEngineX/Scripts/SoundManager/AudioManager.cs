using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

namespace UnityEngineX
{
    public class AudioManager : SingletonBehaviour<AudioManager>
    {
        public static bool IgnoreDuplicateMusic { get; set; }
        public static bool IgnoreDuplicateSounds { get; set; }
        public static bool IgnoreDuplicateUISounds { get; set; }
        public static float GlobalVolume { get; set; }
        public static float GlobalMusicVolume { get; set; }
        public static float GlobalSoundsVolume { get; set; }
        public static float GlobalUISoundsVolume { get; set; }
        
        private static Dictionary<int, Audio> musicAudio;
        private static Dictionary<int, Audio> soundsAudio;
        private static Dictionary<int, Audio> UISoundsAudio;
        private static Dictionary<int, Audio> audioPool;

        private static bool _initialized = false;
        
        private void OneTimeInit()
        {
            if (_initialized) return;
            musicAudio = new Dictionary<int, Audio>();
            soundsAudio = new Dictionary<int, Audio>();
            UISoundsAudio = new Dictionary<int, Audio>();
            audioPool = new Dictionary<int, Audio>();

            GlobalVolume = 1;
            GlobalMusicVolume = 1;
            GlobalSoundsVolume = 1;
            GlobalUISoundsVolume = 1;

            IgnoreDuplicateMusic = false;
            IgnoreDuplicateSounds = false;
            IgnoreDuplicateUISounds = false;

            _initialized = true;
        }

        private void EnsureAudioPlayer()
        {
            if (AudioPlayer.instance == null) { Instantiate(Resources.Load("AudioPlayer")); }
        }
        
        private void OnEnable()
        {
            EnsureAudioPlayer();
            OneTimeInit();
        }

        public static void UpdateAudio()
        {
            UpdateAllAudio( musicAudio );
            UpdateAllAudio( soundsAudio );
            UpdateAllAudio( UISoundsAudio );
        }
        
        private static Dictionary<int, Audio> GetAudioTypeDictionary( Audio.AudioType audioType )
        {
            Dictionary<int, Audio> audioDict = new Dictionary<int, Audio>();
            switch (audioType)
            {
                case Audio.AudioType.Music:
                    audioDict = musicAudio;
                    break;
                case Audio.AudioType.Sound:
                    audioDict = soundsAudio;
                    break;
                case Audio.AudioType.UISound:
                    audioDict = UISoundsAudio;
                    break;
            }

            return audioDict;
        }
        
        private static bool GetAudioTypeIgnoreDuplicateSetting( Audio.AudioType audioType )
        {
            switch (audioType)
            {
                case Audio.AudioType.Music:
                    return IgnoreDuplicateMusic;
                case Audio.AudioType.Sound:
                    return IgnoreDuplicateSounds;
                case Audio.AudioType.UISound:
                    return IgnoreDuplicateUISounds;
                default:
                    return false;
            }
        }
        
        private static void UpdateAllAudio( Dictionary<int, Audio> audioDict )
        {
            // Go through all audios and update them
            List<int> keys = new List<int>( audioDict.Keys );
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                audio.Update();

                // Remove it if it is no longer active (playing)
                if (!audio.IsPlaying && !audio.Paused)
                {
                    Destroy( audio.AudioSource );

                    // Add it to the audio pool in case it needs to be referenced in the future
                    audioPool.Add( key, audio );
                    audio.Pooled = true;
                    audioDict.Remove( key );
                }
            }
        }

        public static void RemoveAllNonPersistAudios()
        {
            RemoveNonPersistAudio( musicAudio );
            RemoveNonPersistAudio( soundsAudio );
            RemoveNonPersistAudio( UISoundsAudio );
        }
        
        private static void RemoveNonPersistAudio( Dictionary<int, Audio> audioDict )
        {
            // Go through all audios and remove them if they should not persist through scenes
            List<int> keys = new List<int>( audioDict.Keys );
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                if (!audio.Persist && audio.Activated)
                {
                    Destroy( audio.AudioSource );
                    audioDict.Remove( key );
                }
            }

            // Go through all audios in the audio pool and remove them if they should not persist through scenes
            keys = new List<int>( audioPool.Keys );
            foreach (int key in keys)
            {
                Audio audio = audioPool[key];
                if (!audio.Persist && audio.Activated)
                {
                    audioPool.Remove( key );
                }
            }
        }
        
        public static bool RestoreAudioFromPool( Audio.AudioType audioType, int audioID )
        {
            if (audioPool.ContainsKey( audioID ))
            {
                Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );
                audioDict.Add( audioID, audioPool[audioID] );
                audioPool.Remove( audioID );

                return true;
            }

            return false;
        }

        #region GetAudio Functions

        public static Audio GetAudio( int audioID )
        {
            Audio audio;

            audio = GetMusicAudio( audioID );
            if (audio != null)
            {
                return audio;
            }

            audio = GetSoundAudio( audioID );
            if (audio != null)
            {
                return audio;
            }

            audio = GetUISoundAudio( audioID );
            if (audio != null)
            {
                return audio;
            }

            return null;
        }
        
        public static Audio GetAudio( AudioClip audioClip )
        {
            Audio audio = GetMusicAudio( audioClip );
            if (audio != null)
            {
                return audio;
            }

            audio = GetSoundAudio( audioClip );
            if (audio != null)
            {
                return audio;
            }

            audio = GetUISoundAudio( audioClip );
            if (audio != null)
            {
                return audio;
            }

            return null;
        }
        
        public static Audio GetMusicAudio( int audioID )
        {
            return GetAudio( Audio.AudioType.Music, true, audioID );
        }

        public static Audio GetMusicAudio( AudioClip audioClip )
        {
            return GetAudio( Audio.AudioType.Music, true, audioClip );
        }

        public static Audio GetSoundAudio( int audioID )
        {
            return GetAudio( Audio.AudioType.Sound, true, audioID );
        }

        public static Audio GetSoundAudio( AudioClip audioClip )
        {
            return GetAudio( Audio.AudioType.Sound, true, audioClip );
        }

        public static Audio GetUISoundAudio( int audioID )
        {
            return GetAudio( Audio.AudioType.UISound, true, audioID );
        }

        public static Audio GetUISoundAudio( AudioClip audioClip )
        {
            return GetAudio( Audio.AudioType.UISound, true, audioClip );
        }

        private static Audio GetAudio( Audio.AudioType audioType, bool usePool, int audioID )
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );

            if (audioDict.ContainsKey( audioID ))
            {
                return audioDict[audioID];
            }

            if (usePool && audioPool.ContainsKey( audioID ) && audioPool[audioID].Type == audioType)
            {
                return audioPool[audioID];
            }

            Debug.LogError( audioType + " with ID " + audioID + " Not Found" );

            return null;
        }

        private static Audio GetAudio( Audio.AudioType audioType, bool usePool, AudioClip audioClip )
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );

            List<int> audioTypeKeys = new List<int>( audioDict.Keys );
            List<int> poolKeys = new List<int>( audioPool.Keys );
            List<int> keys = usePool ? audioTypeKeys.Concat( poolKeys ).ToList() : audioTypeKeys;
            foreach (int key in keys)
            {
                Audio audio = null;
                if (audioDict.ContainsKey( key ))
                {
                    audio = audioDict[key];
                }
                else if (audioPool.ContainsKey( key ))
                {
                    audio = audioPool[key];
                }
                if (audio == null)
                {
                    return null;
                }
                if (audio.Clip == audioClip && audio.Type == audioType)
                {
                    return audio;
                }
            }

            return null;
        }

        #endregion

        #region Prepare Function

        public static int PrepareMusic( AudioClip clip )
        {
            return PrepareAudio( Audio.AudioType.Music, clip, 1f, false, false, 1f, 1f, -1f, null );
        }

        public static int PrepareMusic( AudioClip clip, float volume )
        {
            return PrepareAudio( Audio.AudioType.Music, clip, volume, false, false, 1f, 1f, -1f, null );
        }

        public static int PrepareMusic( AudioClip clip, float volume, bool loop, bool persist )
        {
            return PrepareAudio( Audio.AudioType.Music, clip, volume, loop, persist, 1f, 1f, -1f, null );
        }

        public static int PrepareMusic( AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds )
        {
            return PrepareAudio( Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f, null );
        }
        
        public static int PrepareMusic( AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform )
        {
            return PrepareAudio( Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform );
        }

        public static int PrepareSound( AudioClip clip )
        {
            return PrepareAudio( Audio.AudioType.Sound, clip, 1f, false, false, 0f, 0f, -1f, null );
        }

        public static int PrepareSound( AudioClip clip, float volume )
        {
            return PrepareAudio( Audio.AudioType.Sound, clip, volume, false, false, 0f, 0f, -1f, null );
        }

        public static int PrepareSound( AudioClip clip, bool loop )
        {
            return PrepareAudio( Audio.AudioType.Sound, clip, 1f, loop, false, 0f, 0f, -1f, null );
        }

        public static int PrepareSound( AudioClip clip, float volume, bool loop, Transform sourceTransform )
        {
            return PrepareAudio( Audio.AudioType.Sound, clip, volume, loop, false, 0f, 0f, -1f, sourceTransform );
        }

        public static int PrepareUISound( AudioClip clip )
        {
            return PrepareAudio( Audio.AudioType.UISound, clip, 1f, false, false, 0f, 0f, -1f, null );
        }

        public static int PrepareUISound( AudioClip clip, float volume )
        {
            return PrepareAudio( Audio.AudioType.UISound, clip, volume, false, false, 0f, 0f, -1f, null );
        }

        private static int PrepareAudio( Audio.AudioType audioType, AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform )
        {
            if (clip == null)
            {
                Debug.LogError( "[Audio Manager] Audio clip is null", clip );
            }

            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );
            bool ignoreDuplicateAudio = GetAudioTypeIgnoreDuplicateSetting( audioType );

            if (ignoreDuplicateAudio)
            {
                Audio duplicateAudio = GetAudio( audioType, true, clip );
                if (duplicateAudio != null)
                {
                    return duplicateAudio.AudioID;
                }
            }

            // Create the audioSource
            Audio audio = new Audio( audioType, clip, loop, persist, volume, fadeInSeconds, fadeOutSeconds, sourceTransform );

            // Add it to dictionary
            audioDict.Add( audio.AudioID, audio );

            return audio.AudioID;
        }

        #endregion

        #region Play Functions
        
        public static int PlayMusic( AudioClip clip )
        {
            return PlayAudio( Audio.AudioType.Music, clip, 1f, false, false, 1f, 1f, -1f, null );
        }

        public static int PlayMusic( AudioClip clip, float volume )
        {
            return PlayAudio( Audio.AudioType.Music, clip, volume, false, false, 1f, 1f, -1f, null );
        }

        public static int PlayMusic( AudioClip clip, float volume, bool loop, bool persist )
        {
            return PlayAudio( Audio.AudioType.Music, clip, volume, loop, persist, 1f, 1f, -1f, null );
        }

        public static int PlayMusic( AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds )
        {
            return PlayAudio( Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f, null );
        }

        public static int PlayMusic( AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform )
        {
            return PlayAudio( Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform );
        }

        public static int PlaySound( AudioClip clip )
        {
            return PlayAudio( Audio.AudioType.Sound, clip, 1f, false, false, 0f, 0f, -1f, null );
        }

        public static int PlaySound( AudioClip clip, float volume )
        {
            return PlayAudio( Audio.AudioType.Sound, clip, volume, false, false, 0f, 0f, -1f, null );
        }

        public static int PlaySound( AudioClip clip, bool loop )
        {
            return PlayAudio( Audio.AudioType.Sound, clip, 1f, loop, false, 0f, 0f, -1f, null );
        }

        public static int PlaySound( AudioClip clip, float volume, bool loop, Transform sourceTransform )
        {
            return PlayAudio( Audio.AudioType.Sound, clip, volume, loop, false, 0f, 0f, -1f, sourceTransform );
        }

        public static int PlayUISound( AudioClip clip )
        {
            return PlayAudio( Audio.AudioType.UISound, clip, 1f, false, false, 0f, 0f, -1f, null );
        }

        public static int PlayUISound( AudioClip clip, float volume )
        {
            return PlayAudio( Audio.AudioType.UISound, clip, volume, false, false, 0f, 0f, -1f, null );
        }

        private static int PlayAudio( Audio.AudioType audioType, AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform )
        {
            int audioID = PrepareAudio( audioType, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform );

            // Stop all current music playing
            if (audioType == Audio.AudioType.Music)
            {
                StopAllMusic( currentMusicfadeOutSeconds );
            }

            GetAudio( audioType, false, audioID ).Play();

            return audioID;
        }

        #endregion

        #region Stop Functions

        public static void StopAll()
        {
            StopAll( -1f );
        }
        
        public static void StopAll( float musicFadeOutSeconds )
        {
            StopAllMusic( musicFadeOutSeconds );
            StopAllSounds();
            StopAllUISounds();
        }
        
        public static void StopAllMusic()
        {
            StopAllAudio( Audio.AudioType.Music, -1f );
        }

        public static void StopAllMusic( float fadeOutSeconds )
        {
            StopAllAudio( Audio.AudioType.Music, fadeOutSeconds );
        }
        
        public static void StopAllSounds()
        {
            StopAllAudio( Audio.AudioType.Sound, -1f );
        }

        public static void StopAllUISounds()
        {
            StopAllAudio( Audio.AudioType.UISound, -1f );
        }

        private static void StopAllAudio( Audio.AudioType audioType, float fadeOutSeconds )
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );

            List<int> keys = new List<int>( audioDict.Keys );
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                if (fadeOutSeconds > 0)
                {
                    audio.FadeOutSeconds = fadeOutSeconds;
                }
                audio.Stop();
            }
        }

        #endregion

        #region Pause Functions
        
        public static void PauseAll()
        {
            PauseAllMusic();
            PauseAllSounds();
            PauseAllUISounds();
        }
        
        public static void PauseAllMusic()
        {
            PauseAllAudio( Audio.AudioType.Music );
        }

        public static void PauseAllSounds()
        {
            PauseAllAudio( Audio.AudioType.Sound );
        }

        public static void PauseAllUISounds()
        {
            PauseAllAudio( Audio.AudioType.UISound );
        }

        private static void PauseAllAudio( Audio.AudioType audioType )
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );

            List<int> keys = new List<int>( audioDict.Keys );
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                audio.Pause();
            }
        }

        #endregion

        #region Resume Functions
        
        public static void ResumeAll()
        {
            ResumeAllMusic();
            ResumeAllSounds();
            ResumeAllUISounds();
        }

        public static void ResumeAllMusic()
        {
            ResumeAllAudio( Audio.AudioType.Music );
        }

        public static void ResumeAllSounds()
        {
            ResumeAllAudio( Audio.AudioType.Sound );
        }

        public static void ResumeAllUISounds()
        {
            ResumeAllAudio( Audio.AudioType.UISound );
        }

        private static void ResumeAllAudio( Audio.AudioType audioType )
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary( audioType );

            List<int> keys = new List<int>( audioDict.Keys );
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                audio.Resume();
            }
        }

        #endregion
    }
}
