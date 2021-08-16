using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugX
{
    public static void Log(string inLog,string inFilterName, GameObject inContext)
    {
        ConsoleProDebug.LogToFilter(inLog,inFilterName,inContext);
    }
}