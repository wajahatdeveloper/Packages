using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonBehaviour<LevelManager>
{
    public List<Level> levels;
    public int CurrentLevel
    {
        get => PlayerPrefs.GetInt("CurrentLevel",1);
        set => PlayerPrefs.SetInt("CurrentLevel",value);
    }

    private void Start()
    {
        levels[CurrentLevel - 1].gameObject.SetActive(true);
    }
}