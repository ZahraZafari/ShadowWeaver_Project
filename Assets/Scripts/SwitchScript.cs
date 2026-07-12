using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public string targetName;
    public bool isPressed = false;
    public bool stayPressed = false;
    public Color activeColor = Color.green;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip switchClip;

    private Color defaultColor;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
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
            PlaySwitchSound();
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

    void PlaySwitchSound()
    {
        if (switchClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(switchClip);
        }
    }
}