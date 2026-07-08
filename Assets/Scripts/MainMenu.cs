using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject tutorialPanel;

    void Start()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowTutorial()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }

    public void HideTutorial()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    public void QuitGame()
{
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
}
}