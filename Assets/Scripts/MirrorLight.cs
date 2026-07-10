using UnityEngine;

public class MirrorLight : MonoBehaviour
{
    public ParticleSystem burnEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Aria")
        {
            // پخش افکت
            if (burnEffect != null)
                burnEffect.Play();

            // برگشت به چک‌پوینت
            if (GameManager.lastCheckpoint != Vector3.zero)
            {
                GameObject aria = GameObject.Find("Aria");
                GameObject shadow = GameObject.Find("Shadow");

                if (aria != null)
                    aria.transform.position = GameManager.lastCheckpoint;

                if (shadow != null)
                    shadow.transform.position = GameManager.lastCheckpoint;
            }

            // متوقف کردن افکت بعد از برگشت
            if (burnEffect != null)
                burnEffect.Stop();
        }
    }
}