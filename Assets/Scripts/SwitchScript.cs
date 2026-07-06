using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public string targetName;
    public bool isPressed = false;
    public bool stayPressed = false;
    public Color activeColor = Color.green;

    private Color defaultColor;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string allowedName = targetName;

        if (GameManager.mirrorMode)
        {
            if (targetName == "Shadow")
                allowedName = "Aria";
            else if (targetName == "Aria")
                allowedName = "Shadow";
        }

        if (other.gameObject.name == allowedName)
        {
            isPressed = true;
            sr.color = activeColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string allowedName = targetName;

        if (GameManager.mirrorMode)
        {
            if (targetName == "Shadow")
                allowedName = "Aria";
            else if (targetName == "Aria")
                allowedName = "Shadow";
        }

        if (other.gameObject.name == allowedName && !stayPressed)
        {
            isPressed = false;
            sr.color = defaultColor;
        }
    }
}