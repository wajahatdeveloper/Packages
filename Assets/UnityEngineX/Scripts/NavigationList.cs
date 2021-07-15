using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NavigationList<T> : IList<T>
{
    private int _currentIndex = 0;
    public List<T> _listImplementation;

    public Action OnSequenceEndReached;
    
    public int CurrentIndex
    {
        get
        {
            if (_currentIndex > Count - 1) { _currentIndex = Count - 1; OnSequenceEndReached?.Invoke(); }
            if (_currentIndex < 0) { _currentIndex = 0; }
            return _currentIndex;
        }
        set { _currentIndex = value; }
    }

    public T MoveNext
    {
        get { _currentIndex++; return this[CurrentIndex]; }
    }

    public T MovePrevious
    {
        get { _currentIndex--; return this[CurrentIndex]; }
    }

    public T Current
    {
        get { return this[CurrentIndex]; }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _listImplementation.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _listImplementation).GetEnumerator();
    }

    public void Add(T item)
    {
        _listImplementation.Add(item);
    }

    public void Clear()
    {
        _listImplementation.Clear();
    }

    public bool Contains(T item)
    {
        return _listImplementation.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _listImplementation.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _listImplementation.Remove(item);
    }

    public int Count => _listImplementation.Count;

    public bool IsReadOnly => false;

    public int IndexOf(T item)
    {
        return _listImplementation.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        _listImplementation.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _listImplementation.RemoveAt(index);
    }

    public T this[int index]
    {
        get => _listImplementation[index];
        set => _listImplementation[index] = value;
    }
}