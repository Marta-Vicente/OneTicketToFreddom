using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }
    void Awake()
    {
        // Needed if we want the audio manager to persist through scenes
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Destroy(instance);
            //instance = this;
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Add audio source components
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }
    
    public Sound[] sounds;
    
    private Sound intro;
    private Sound loop;

    private void Start()
    {
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if (!s.IsPlaying())
            s.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.Stop();
    }

    public void SetMusic(string introName, string loopName)
    {
        if (intro != null && introName == intro.name && loop != null && loopName == loop.name)
        {
            Debug.Log(introName + "/" + loopName + " already playing, ignoring.");
            return;
        }
        if (intro != null)
            intro.Stop();
        if (loop != null)
            loop.Stop();

        intro = Array.Find(sounds, sound => sound.name == introName);
        if (intro == null)
        {
            Debug.LogWarning("Sound " + introName + " not found!");
            return;
        }

        loop = Array.Find(sounds, sound => sound.name == loopName);
        if (intro == null)
        {
            Debug.LogWarning("Sound " + loopName + " not found!");
            return;
        }

        double introDuration = intro.clip.length;
        double startTime = AudioSettings.dspTime + 0.2;
        intro.PlayScheduled(startTime);
        loop.PlayScheduled(startTime + introDuration);
    }

    public bool IsPlaying(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        return s.IsPlaying();
    }

    private IEnumerator PlayRandomAmbience()
    {
        yield return new WaitForSeconds(Random.Range(20, 41));
        var rng = Random.Range(1, 3);
        Play(rng == 1 ? "ExplorationMusic1" : "ExplorationMusic2");
    }

    public void ChangePitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.SetPitch(pitch);
        }
    }
}
