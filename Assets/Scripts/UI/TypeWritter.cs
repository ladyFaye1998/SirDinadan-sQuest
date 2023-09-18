using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TypeWritter : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public float typingSpeed = 50f;
    private float delayBeforeDestroying = 2f;
    private float fadeInDuration = 2f;
    private float fadeOutDuration = 2f;

    public GameObject nextText;
    public GameObject player;
    public GameObject introCanvas;

    private string fullText;
    private string currentText = "";
    public AudioSource typingAudio;
    public AudioSource SkippedAudio;

    public bool isFinish = false;
    public bool type = true;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Skip();
        }
    }

    private void Start()
    {
        if (type)
        {
            fullText = textMeshPro.text;
            textMeshPro.text = ""; // Clear the text initially
            StartCoroutine(TypeText());
            typingAudio.Play();
        }
        else
        {
            if (textMeshPro != null)
            {
                // Set the initial text opacity to 0
                CanvasGroup canvasGroup = textMeshPro.GetComponent<CanvasGroup>();

                if (canvasGroup == null)
                {
                    canvasGroup = textMeshPro.gameObject.AddComponent<CanvasGroup>();
                }

                canvasGroup.alpha = 0f;
                StartCoroutine(FadeInText());
            }
        }
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(1f / typingSpeed);
        }
        typingAudio.Stop();

        yield return new WaitForSeconds(delayBeforeDestroying);
        FinishedAnimation();
        
    }


    private IEnumerator FadeInText()
    {
        CanvasGroup canvasGroup = textMeshPro.GetComponent<CanvasGroup>();

        // Make sure the CanvasGroup is initialized
        if (canvasGroup == null)
        {
            canvasGroup = textMeshPro.gameObject.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;
        canvasGroup.alpha = 0f;

        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is fully visible
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(2f);

        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        float elapsedTimeout = 0f;

        while (elapsedTimeout < fadeOutDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTimeout / fadeOutDuration);
            elapsedTimeout += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is fully invisible
        canvasGroup.alpha = 0f;

        FinishedAnimation();
    }

    private void FinishedAnimation()
    {


        if(nextText != null)
        {
            nextText.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            if (isFinish)
            {
                GameFinished();
            }
            else
            {
                player.SetActive(true);
                Destroy(introCanvas);
            }

        }
    }

    public void Skip()
    {
        SkippedAudio.Play();
        player.SetActive(true);
        Destroy(introCanvas);
    }

    public void GameFinished()
    {
        SceneManager.LoadScene(0);
    }

}