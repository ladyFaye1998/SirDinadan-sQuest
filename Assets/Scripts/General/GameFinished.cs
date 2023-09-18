using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    public float delay;
    public GameObject gameFinishedCanvas;

    public AudioSource audioSource;
    public float fadeDuration = 2f;

    private float initialVolume;



    private void Start()
    {
        StartCoroutine(loadText());

        if (audioSource != null)
        {
            initialVolume = audioSource.volume;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator loadText()
    {
        yield return new WaitForSeconds(delay);
        if (gameFinishedCanvas != null)
        {
            gameFinishedCanvas.SetActive(true);
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            audioSource.volume = Mathf.Lerp(initialVolume, 0f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is fully set to 0
        audioSource.volume = 0f;

        // Optionally, you can stop the audio source or perform other actions here
        audioSource.Stop();
    }


}
