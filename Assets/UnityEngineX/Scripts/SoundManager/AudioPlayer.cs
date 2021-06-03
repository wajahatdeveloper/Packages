using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineX;

public class AudioPlayer : SingletonBehaviour<AudioPlayer>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded( Scene scene, LoadSceneMode mode )
    {
        // Stop and remove all non-persistent audio
        AudioManager.RemoveAllNonPersistAudios();
    }

    private void Update()
    {
        AudioManager.UpdateAudio();
    }
}