using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject door;
    public SwitchScript ariaSwitch;
    public SwitchScript shadowSwitch;

    void Update()
    {
        if (ariaSwitch.isPressed && shadowSwitch.isPressed)
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }
}