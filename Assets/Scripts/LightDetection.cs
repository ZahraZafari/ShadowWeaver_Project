using UnityEngine;
using System.Collections;

public class LightDetection : MonoBehaviour
{
    public ParticleSystem burnEffect;
    public float respawnDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Light hit: " + other.gameObject.name);

        if (other.gameObject.name == "Shadow")
        {
            StartCoroutine(BurnAndRespawn());
        }
    }

    IEnumerator BurnAndRespawn()
    {
        if (burnEffect != null)
        {
            burnEffect.Play();
            Debug.Log("Burn effect played!");
        }

        yield return new WaitForSeconds(respawnDelay);

        Debug.Log("Last checkpoint: " + GameManager.lastCheckpoint);

        if (GameManager.lastCheckpoint != Vector3.zero)
        {
            GameObject aria = GameObject.Find("Aria");
            GameObject shadow = GameObject.Find("Shadow");

            if (aria != null)
            {
                aria.transform.position = GameManager.lastCheckpoint;
                Debug.Log("Aria moved to checkpoint");
            }

            if (shadow != null)
            {
                shadow.transform.position = GameManager.lastCheckpoint;
                Debug.Log("Shadow moved to checkpoint");
            }
        }
        else
        {
            Debug.LogWarning("Last checkpoint is zero! Set a checkpoint first.");
        }
    }
}