using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string allowedPlayer;

        if (GameManager.mirrorMode)
            allowedPlayer = "Aria";
        else
            allowedPlayer = "Shadow";

        if (other.gameObject.name == allowedPlayer)
        {
            door.SetActive(false);
        }
    }
}