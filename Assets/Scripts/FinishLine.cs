using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip winClip;

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
            PlayWinSound();
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        float waitTime = winClip != null ? Mathf.Min(winClip.length, 2f) : 1f;
        yield return new WaitForSeconds(waitTime);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void PlayWinSound()
    {
        if (winClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(winClip);
        }
    }
}