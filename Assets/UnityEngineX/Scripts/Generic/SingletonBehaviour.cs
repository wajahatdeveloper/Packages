using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }
            return instance;
        }
    }

    /*public virtual void Awake()
    {
        if (instance)
        {
            Debug.LogError("Duplicate subclass of type " + typeof(T) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
        }
    }*/

    /*public virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }*/
}