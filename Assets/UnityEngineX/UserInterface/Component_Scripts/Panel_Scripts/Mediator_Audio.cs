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

	public UnityEvent<float> OnSoundValueChanged;
	public UnityEvent<float> OnMusicValueChanged;
	public UnityEvent<float> OnAudioValueChanged;

    #region Properties
    protected bool SoundState
    {
        get => PlayerPrefsX.GetBool("sound", true);
        set => PlayerPrefsX.SetBool("sound", value);
    }

    protected bool MusicState
    {
        get => PlayerPrefsX.GetBool("music", true);
        set => PlayerPrefsX.SetBool("music", value);
    }

    protected bool AudioState
    {
        get => PlayerPrefsX.GetBool("audio", true);
        set => PlayerPrefsX.SetBool("audio", value);
    }

    protected float SoundValue
    {
		get => PlayerPrefs.GetFloat( "sound", 1.0f );
		set => PlayerPrefs.SetFloat( "sound", value );
	}

    protected float MusicValue
	{
		get => PlayerPrefs.GetFloat( "music", 1.0f );
		set => PlayerPrefs.SetFloat( "music", value );
	}

    protected float AudioValue
    {
		get => PlayerPrefs.GetFloat( "audio", 1.0f );
		set => PlayerPrefs.SetFloat( "audio", value );
	}
#endregion

	private void Start()
    {
        if (rememberLastState)
        {
            bool soundState = SoundState;
            bool musicState = MusicState;
            bool audioState = AudioState;

            SetSoundState(soundState);
            SetMusicState(musicState);
            SetAudioState(audioState);

			float soundValue = SoundValue;
			float musicValue = MusicValue;
			float audioValue = AudioValue;

			SetSoundValue( soundValue );
			SetMusicValue( musicValue );
			SetAudioValue( audioValue );
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

	public void SetSoundValue( float val )
	{
        AudioController.SetCategoryVolume( "SFX", val );
		if (rememberLastState) { SoundValue = val; }
		OnSoundValueChanged?.Invoke( val );
	}

	public void SetMusicValue( float val )
	{
		AudioController.SetMusicVolume( val );
		if (rememberLastState) { MusicValue = val; }
        OnMusicValueChanged?.Invoke( val );
	}

	public void SetAudioValue( float val )
	{
		AudioController.SetGlobalVolume( val );
		if (rememberLastState) { AudioValue = val; }
        OnAudioValueChanged?.Invoke( val );
	}
}