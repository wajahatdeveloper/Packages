using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStream
{
    public static Dictionary<object, DataStream> StreamMap = new Dictionary<object, DataStream>();
    
    [System.Serializable]
    public class DataPacket
    {
        public Object[] unityDataArray = null;
        public object[] plainDataArray = null;
    }

    private DataStream _otherPoint;
    private Queue<DataPacket> _dataPipeline = null;

    public DataStream(object key)
    {
        if (StreamMap.ContainsKey(key))
        {
            _otherPoint = StreamMap[key];
            _dataPipeline = new Queue<DataPacket>();
            _otherPoint._dataPipeline = _dataPipeline;
            StreamMap.Remove(key);

            if (StreamMap.ContainsKey(key))
            {
                Debug.LogError("Multi point Data Stream is not supported : " + key);
            }
        }
        else
        {
            StreamMap.Add(key, this);
        }
    }

    public void Clear()
    {
        _dataPipeline.Clear();
    }
    
    public void Push(DataPacket dataPacket)
    {
        _dataPipeline.Enqueue(dataPacket);
    }
    
    public void Push(params object[] plainDataArray)
    {
        DataPacket dataPacket = new DataPacket {plainDataArray = plainDataArray};
        _dataPipeline.Enqueue(dataPacket);
    }
    
    public DataPacket Pull()
    {
        return _dataPipeline.DequeueOrNull();
    }
    
    public DataPacket Peek()
    {
        return _dataPipeline.Peek();
    }
}