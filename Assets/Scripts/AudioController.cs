using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController 
{
    private static AudioController singleton;

    private ComponentPooling<AudioSource> pooling;

    public static AudioController Singleton => singleton ?? (singleton = new AudioController());

    private AudioController() 
    {
        var prefab = new GameObject().AddComponent<AudioSource>();

        pooling = new ComponentPooling<AudioSource>(prefab, 3);
    }

    public AudioSource GetSource() 
    {
        var instance = Singleton.pooling.GetValue();

        Coroutines.Start(singleton.DestroyInstance(instance));

        return instance;
    }

    private IEnumerator DestroyInstance(AudioSource instance) 
    {
        yield return new WaitWhile(()=> instance.isPlaying);

        pooling.Destroy(instance);
    }
}
