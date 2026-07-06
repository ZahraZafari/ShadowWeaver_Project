using UnityEngine;
using System.Collections;

public class LightDetection : MonoBehaviour
{
    public ParticleSystem burnEffect;
    public float respawnDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Shadow")
        {
            StartCoroutine(BurnAndRespawn());
        }
    }

    IEnumerator BurnAndRespawn()
    {
        if (burnEffect != null)
            burnEffect.Play();

        yield return new WaitForSeconds(respawnDelay);

        if (GameManager.lastCheckpoint != Vector3.zero)
        {
            GameObject aria = GameObject.Find("Aria");
            GameObject shadow = GameObject.Find("Shadow");

            if (aria != null)
                aria.transform.position = GameManager.lastCheckpoint;

            if (shadow != null)
                shadow.transform.position = GameManager.lastCheckpoint;
        }
    }
}