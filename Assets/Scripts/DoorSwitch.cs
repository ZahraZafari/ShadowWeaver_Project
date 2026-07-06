using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Shadow")
        {
            door.SetActive(false);
        }
    }
}