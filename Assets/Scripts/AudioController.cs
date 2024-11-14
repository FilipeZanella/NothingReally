using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController 
{
    private static AudioController singleton;

    private ComponentPooling<AudioSource> pooling;

    public static AudioController Singleton => singleton ?? (singleton = new AudioController());

    private AudioController() 
    {
        SceneManager.sceneLoaded += (s, u) =>pooling.Clear();

        pooling = new ComponentPooling<AudioSource>(3);
    }

    public AudioSource GetSource() 
    {
        var instance = Singleton.pooling.GetValue();

        Coroutines.Start(singleton.DestroyInstance(instance));

        return instance;
    }

    private IEnumerator DestroyInstance(AudioSource instance)
    {
        yield return new WaitWhile(() => instance && instance.isPlaying);

        if (instance)
        {
            pooling.Destroy(instance);
        }
    }
}
