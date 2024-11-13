using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectContainer : MonoBehaviour
{
    public static SceneObjectContainer singleton;

    [SerializeField] private List<ViewPanel> views;

    private void Awake()
    {
        if (singleton)
        {
            Destroy(singleton.gameObject);
        }

        singleton = this;
    }

    public T GetViewPanel<T>() where T : ViewPanel 
    {
        foreach (var panel in views) 
        {
            if (panel is T d)
                return d;
        }

        return null;
    }
}
