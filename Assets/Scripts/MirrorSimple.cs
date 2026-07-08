using UnityEngine;

public class MirrorSimple : MonoBehaviour
{
    public GameObject realWorld;
    public GameObject mirrorWorld;

    private bool mirrored = false;
    private bool canSwitch = true;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (realWorld != null)
            realWorld.SetActive(true);
        if (mirrorWorld != null)
            mirrorWorld.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!canSwitch)
            return;

        canSwitch = false;

        mirrored = !mirrored;

        if (realWorld != null)
            realWorld.SetActive(!mirrored);

        if (mirrorWorld != null)
            mirrorWorld.SetActive(mirrored);

        if (sr != null)
            sr.color = mirrored ? Color.green : Color.red;

        Debug.Log("Mirror toggled: " + mirrored);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canSwitch = true;
        }
    }
}