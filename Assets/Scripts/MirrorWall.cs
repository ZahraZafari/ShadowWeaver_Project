using UnityEngine;

public class MirrorWall : MonoBehaviour
{
    private Collider2D wallCollider;

    void Start()
    {
        wallCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        GameObject aria = GameObject.Find("Aria");
        GameObject shadow = GameObject.Find("Shadow");

        if (aria == null || shadow == null) return;

        if (Vector3.Distance(aria.transform.position, shadow.transform.position) < 0.1f)
        {
            wallCollider.enabled = false;
        }
        else
        {
            wallCollider.enabled = true;
        }
    }
}