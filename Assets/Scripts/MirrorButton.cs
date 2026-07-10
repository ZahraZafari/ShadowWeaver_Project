using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MirrorButton : MonoBehaviour
{
    public GameObject fadePanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Aria")
        {
            StartCoroutine(LoadSceneWithFade());
        }
    }

    IEnumerator LoadSceneWithFade()
    {
        // نمایش صفحه سیاه
        if (fadePanel != null)
            fadePanel.SetActive(true);

        // مکث کوتاه برای نمایش fade
        yield return new WaitForSeconds(0.3f);

        // تعیین صحنه بعدی
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = (currentScene == "Level3") ? "Level3_Mirror" : "Level3";

        // لود صحنه
        SceneManager.LoadScene(nextScene);
    }
}