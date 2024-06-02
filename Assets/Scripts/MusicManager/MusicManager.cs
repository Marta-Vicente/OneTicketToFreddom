using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource musicSource1;
    public AudioSource musicSource2;

    private bool isPlayingSource1 = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip newClip, float fadeDuration = 1.0f)
    {
        StartCoroutine(FadeMusic(newClip, fadeDuration));
    }

    private IEnumerator FadeMusic(AudioClip newClip, float fadeDuration)
    {
        AudioSource activeSource = isPlayingSource1 ? musicSource1 : musicSource2;
        AudioSource inactiveSource = isPlayingSource1 ? musicSource2 : musicSource1;

        inactiveSource.clip = newClip;
        inactiveSource.Play();

        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            activeSource.volume = 1 - t / fadeDuration;
            inactiveSource.volume = t / fadeDuration;
            yield return null;
        }

        activeSource.Stop();
        activeSource.volume = 1.0f;
        inactiveSource.volume = 1.0f;

        isPlayingSource1 = !isPlayingSource1;
    }
}
