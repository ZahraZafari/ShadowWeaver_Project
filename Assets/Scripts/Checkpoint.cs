using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Aria")
        {
            
            GameManager.lastCheckpoint = transform.position;
            Debug.Log("Checkpoint saved!");
        }
    }
}