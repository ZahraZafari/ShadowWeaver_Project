using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MirrorButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Aria")
        {
            string currentScene = SceneManager.GetActiveScene().name;
            
            if (currentScene == "Level3")
            {
                SceneManager.LoadScene("Level3_Mirror");
            }
            else if (currentScene == "Level3_Mirror")
            {
                SceneManager.LoadScene("Level3");
            }
        }
    }
}