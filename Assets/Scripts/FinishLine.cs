using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip nextLevelClip;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Aria")
        {
            StartCoroutine(LevelTransition());
        }
    }

    IEnumerator LevelTransition()
    {
        if (winClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(winClip);
            yield return new WaitForSeconds(winClip.length);
        }

        if (nextLevelClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(nextLevelClip);
            yield return new WaitForSeconds(nextLevelClip.length);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}