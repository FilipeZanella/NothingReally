using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentPooling<T> where T : Component
{
    public class PoolingObject 
    {
        public T poolingObject;
        public bool isUsing;
    }

    [SerializeField] private List<PoolingObject> data = new List<PoolingObject>();

    private Transform holder;
    private T prefab;

    public ComponentPooling(T _prefab, int initialCount)
    {
        prefab = _prefab;

        holder = new GameObject().transform;
        holder.name = typeof(T).Name + "_PoolingHolder";

        for (int i = 0; i < initialCount; i++) 
        {
            GetNewInstance();
        }
    }

    protected PoolingObject GetNewInstance() 
    {
        var obj = new PoolingObject();
        obj.poolingObject = UnityEngine.Object.Instantiate(prefab, holder);
        data.Add(obj);

        return obj;
    }

    public void Destroy(T instance) 
    {
        var obj = data.First(x => x.poolingObject == instance);

        if(obj != null) 
        {
            obj.isUsing = false;
        }
    }

    public T GetValue() 
    {
        foreach (var i in data) 
        {
            if (!i.isUsing) 
            {
                i.isUsing = true;

                return i.poolingObject;
            }
        }

        var _new = GetNewInstance();
        _new.isUsing = true;

        return _new.poolingObject;
    }
}
