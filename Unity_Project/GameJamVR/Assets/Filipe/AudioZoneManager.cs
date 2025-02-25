
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AudioZoneManager : MonoBehaviour
{
    public AudioSource previousAudioSource;
    public AudioSource currentAudioSource;  
    public float fadeDuration = 6f;        // Duration of the fade effect


    public void PlaySong(AudioClip newZoneClip, float startingTime)
    {
        if (currentAudioSource.clip != newZoneClip)
        {
            StartCoroutine(FadeToNewTrack(newZoneClip, startingTime));
        }
    }

    private System.Collections.IEnumerator FadeToNewTrack(AudioClip newClip, float startingTime)
    {

        AudioSource tempSource = previousAudioSource;
        previousAudioSource = currentAudioSource;
        currentAudioSource = tempSource;

        //Crossfade
        float elapsedTime = 0f;
        float startVolume = previousAudioSource.volume;
        currentAudioSource.volume = 0f;
        currentAudioSource.clip = newClip;
        currentAudioSource.time = startingTime;
        currentAudioSource.Play();

        while (elapsedTime < fadeDuration)
        {
            previousAudioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            currentAudioSource.volume = 1-Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentAudioSource.volume = startVolume;
    }
}