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

    private object _key;
    private bool _isConnected = false;
    private DataStream _otherPoint;
    private Queue<DataPacket> _dataPipeline = null;

    public DataStream(object key)
    {
        _key = key;
        if (StreamMap.ContainsKey(key))
        {
            _otherPoint = StreamMap[key];
            _dataPipeline = new Queue<DataPacket>();
            _otherPoint._dataPipeline = _dataPipeline;
            StreamMap.Remove(key);
            _isConnected = true;
            _otherPoint._isConnected = true;

            if (StreamMap.ContainsKey(key))
            {
                Debug.LogError("Unable to Connect, Multi point Data Stream is not supported : " + key);
            }
        }
        else
        {
            StreamMap.Add(key, this);
        }
    }

    public void Clear()
    {
        if (!_isConnected)
        {
            Debug.LogError("Unable to Clear, Data Stream not connected : " + _key);
            return;
        }
        _dataPipeline.Clear();
    }
    
    public void Push(DataPacket dataPacket)
    {
        if (!_isConnected)
        {
            Debug.LogError("Unable to Push, Data Stream not connected : " + _key);
            return;
        }
        
        _dataPipeline.Enqueue(dataPacket);
    }
    
    public void Push(params object[] plainDataArray)
    {
        if (!_isConnected)
        {
            Debug.LogError("Unable to Push, Data Stream not connected : " + _key);
            return;
        }

        DataPacket dataPacket = new DataPacket {plainDataArray = plainDataArray};
        _dataPipeline.Enqueue(dataPacket);
    }
    
    public DataPacket Pull()
    {
        if (!_isConnected)
        {
            Debug.LogError("Unable to Pull, Data Stream not connected : " + _key);
            return null;
        }

        return _dataPipeline.DequeueOrNull();
    }
    
    public DataPacket Peek()
    {
        if (!_isConnected)
        {
            Debug.LogError("Unable to Peek, Data Stream not connected : " + _key);
            return null;
        }

        return _dataPipeline.Peek();
    }
}