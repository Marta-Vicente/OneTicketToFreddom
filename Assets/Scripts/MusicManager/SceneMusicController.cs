using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    public AudioClip sceneMusic;

    void Start()
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayMusic(sceneMusic, 2.0f); // 2.0f is the fade duration
        }
    }
}
