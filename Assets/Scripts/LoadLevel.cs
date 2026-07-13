using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void GoToLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void GoToMainMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(0);
}

}