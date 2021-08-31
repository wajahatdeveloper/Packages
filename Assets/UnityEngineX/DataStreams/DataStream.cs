using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStream
{
    [System.Serializable]
    public class DataPacket
    {
        public GameObject sender;
        public Object unityData;
        public object plainData;
    }
    
    private static Dictionary<string,Queue<DataPacket>> _DataPipeline = new Dictionary<string, Queue<DataPacket>>();
    
    // Returns true if created successfully else returns false if already exists
    public static bool Open(string id)
    {
        return _DataPipeline.AddIfNotContainsKey(id, new Queue<DataPacket>());
    }
    
    // Returns true if created successfully else returns false if already exists
    public static void Close(string id)
    {
        _DataPipeline.RemoveIfContainsKey(id);
    }

    // Returns true if pushed successfully else returns false if stream not found
    public static bool Push(string id, DataPacket dataPacket)
    {
        if (_DataPipeline.ContainsKey(id) == false) { return false; }
        
        _DataPipeline[id].Enqueue(dataPacket);
        return true;
    }
    
    public static bool Push(string id, params object[] plainData)
    {
        if (_DataPipeline.ContainsKey(id) == false) { return false; }

        DataPacket dataPacket = new DataPacket {plainData = plainData};
        _DataPipeline[id].Enqueue(dataPacket);
        return true;
    }
    
    // Returns data packet if pulled successfully else returns null if stream not found
    public static DataPacket Pull(string id)
    {
        return _DataPipeline.ContainsKey(id) == false ? null : _DataPipeline[id].DequeueOrNull();
    }
    
    // Returns data packet if pulled successfully else returns null if stream not found
    public static DataPacket Peek(string id)
    {
        return _DataPipeline.ContainsKey(id) == false ? null : _DataPipeline[id].Peek();
    }
}