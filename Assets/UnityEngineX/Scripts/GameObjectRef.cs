using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRef
{
    public string LogTag = "";
    public string LogColor = "";
    public bool LogShow = true;

    public static bool useExceptions = true;
    
    private GameObject _reference;

    public GameObjectRef(GameObject referencedObject = null, string logTag = "", string logColor = "gray", bool logShow = true)
    {
        _reference = referencedObject;
        LogTag = logTag;
        LogColor = logColor;
        LogShow = logShow;
    }

    public bool IsNull()
    {
        return _reference == null;
    }
    
    public bool IsNotNull()
    {
        return _reference != null;
    }
    
    public GameObject Get(string errorIfNull = "<b>Reference is Null</b>", bool ignoreError = false)
    {
        if (_reference.IsNull() && !ignoreError)
        {
            if (!useExceptions)
            {
                #if CONSOLE_PRO
                ConsoleProDebug.LogToFilter($"<b>Reference Null Error : {LogTag}</b>","References");
                #else
                Debug.LogError($"<b>Reference Null Error : {LogTag}</b>");
                #endif
            }
            else
            {
                throw new NullReferenceException(LogTag + " : " + errorIfNull);
            }
        }

        return _reference;
    }
    
    public void Set(GameObject other)
    {
        if (LogShow)
        {
            #if CONSOLE_PRO
            ConsoleProDebug.LogToFilter($"<color={LogColor}>Reference : {LogTag} Set to <b>{((other == null) ? "null" : other.name)}</b> <--- <b>{((_reference == null) ? "null" : _reference.name)}</b></color>","References");
            #else
            Debug.Log($"<color={LogColor}>Reference : {LogTag} Set to <b>{((other == null) ? "null" : other.name)}</b> <--- <b>{((_reference == null) ? "null" : _reference.name)}</b></color>");
            #endif
        }
        _reference = other;
    }
    
    public void Clear()
    {
        if (LogShow)
        {
            #if CONSOLE_PRO
            ConsoleProDebug.LogToFilter($"<color={LogColor}>Reference : {LogTag} Cleared to <b>null</b> <--- <b>{((_reference.IsNull()) ? "null" : _reference.name)}</b></color>","References");
            #else
            Debug.Log($"<color={LogColor}>Reference : {LogTag} Cleared to <b>null</b> <--- <b>{((_reference.IsNull()) ? "null" : _reference.name)}</b></color>");
            #endif
        }
        _reference = null;
    }
}