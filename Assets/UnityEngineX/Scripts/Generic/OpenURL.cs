using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour 
{
    public void OpenURL_Link(string url)
    {
        Application.OpenURL(url);
        Debug.Log($"URL {url} Opened in Browser");
    }
}