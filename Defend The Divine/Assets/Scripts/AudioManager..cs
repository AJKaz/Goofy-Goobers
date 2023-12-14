using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public List<Sound> sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundName)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        Debug.Log("Playing sound " + soundName);
        source.name = soundName;
        source.loop = false;
        AudioSource storedSource = sounds.Find(x => x.name == soundName).source;
        source.volume = storedSource.volume;
        source.clip = storedSource.clip;
        Destroy(source, source.clip.length);
        source.Play();
    }
}
