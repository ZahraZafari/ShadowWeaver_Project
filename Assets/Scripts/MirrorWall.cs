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

        // اگه آریا و سایه روی هم باشن (یعنی سوئیچ شدن)، دیوار رو غیرفعال کن
        if (Vector3.Distance(aria.transform.position, shadow.transform.position) < 0.1f)
        {
            wallCollider.enabled = false; // آریا رد شه
        }
        else
        {
            wallCollider.enabled = true; // سایه رد شه، آریا نه
        }
    }
}