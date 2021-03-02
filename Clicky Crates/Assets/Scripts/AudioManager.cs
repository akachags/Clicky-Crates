using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundClass[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (SoundClass s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("The sound " + name + "was not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
