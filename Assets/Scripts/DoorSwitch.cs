using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // فقط اگر سایه روی دکمه رفت، در باز شود
        if (other.gameObject.name == "Shadow")
        {
            door.SetActive(false); 
        }
    }
}