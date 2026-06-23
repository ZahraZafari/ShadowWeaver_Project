using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // این خط برای جابجایی بین مراحل است

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // چک می‌کند که آیا آریا به خط پایان رسیده یا نه
        if (other.gameObject.name == "Aria")
        {
            // رفتن به مرحله بعدی
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}