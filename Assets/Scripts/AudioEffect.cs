using UnityEngine;

[System.Serializable]
public class AudioEffect 
{
    public AudioClip clip;
    public float volume;
    public float pitch;

    public void ApplyEffect()
    {
        var source = AudioController.Singleton.GetSource();
        source.volume = volume;
        source.pitch = pitch;
        source.clip = clip;

        source.Play();
    }
}
