using UnityEngine;
using UnityEngine.Events;

public class Mediator_Audio : MonoBehaviour
{
    public static bool soundPaused = false;
    public static bool musicPaused = false;
    public static bool audioPaused = false;

    public bool rememberLastState = false;

    public UnityEvent<bool> OnSoundStateChanged;
    public UnityEvent<bool> OnMusicStateChanged;
    public UnityEvent<bool> OnAudioStateChanged;

    public bool SoundState
    {
        get => PlayerPrefsX.GetBool("sound", true);
        set => PlayerPrefsX.SetBool("sound", value);
    }
    
    public bool MusicState
    {
        get => PlayerPrefsX.GetBool("music", true);
        set => PlayerPrefsX.SetBool("music", value);
    }
    
    public bool AudioState
    {
        get => PlayerPrefsX.GetBool("audio", true);
        set => PlayerPrefsX.SetBool("audio", value);
    }
    
    private void Start()
    {
        if (rememberLastState)
        {
            bool sound = SoundState;
            bool music = MusicState;
            bool audio = AudioState;

            SetSoundState(sound);
            SetMusicState(music);
            SetAudioState(audio);
        }
    }

    public void Toggle_Sound()
    {
        SetSoundState(!soundPaused);
    }

    public void Toggle_Music()
    {
        SetMusicState(!musicPaused);
    }

    public void Toggle_Audio()
    {
        SetAudioState(!audioPaused);
    }

    public bool IsSoundEnabled()
    {
        return soundPaused;
    }

    public bool IsMusicEnabled()
    {
        return musicPaused;
    }

    public bool IsAudioEnabled()
    {
        return audioPaused;
    }
    
    public void SetSoundState(bool enable)
    {
        soundPaused = !enable;
        if (enable) { AudioController.UnpauseCategory("SFX"); } else { AudioController.PauseCategory("SFX"); }
        if (rememberLastState) { SoundState = enable; }
        OnSoundStateChanged?.Invoke(enable);
    }
    
    public void SetMusicState(bool enable)
    {
        musicPaused = !enable;
        if (enable) { AudioController.UnpauseMusic(); } else { AudioController.PauseMusic(); }
        if (rememberLastState) { MusicState = enable; }
        OnMusicStateChanged?.Invoke(enable);
    }
    
    public void SetAudioState(bool enable)
    {
        audioPaused = !enable;
        if (enable) { AudioController.PauseAll(); } else { AudioController.UnpauseAll(); }
        if (rememberLastState) { AudioState = enable; }
        OnAudioStateChanged?.Invoke(enable);
    }
}