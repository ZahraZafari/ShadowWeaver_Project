using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Aria")
        {
            other.transform.position = GameManager.lastCheckpoint;
        }
    }
}