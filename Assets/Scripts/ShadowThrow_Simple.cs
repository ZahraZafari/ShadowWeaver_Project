using UnityEngine;

public class ShadowThrow_Simple : MonoBehaviour
{
    public float throwSpeed = 15f;
    public float maxDistance = 8f;
    public LineRenderer aimLine;

    private bool isThrowing = false;
    private Vector3 targetPosition;
    private bool isShadowMode = false;
    private bool isAiming = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShadowMode = !isShadowMode;
            if (!isShadowMode)
            {
                if (aimLine != null)
                    aimLine.enabled = false;
                isAiming = false;
            }
        }

        if (isShadowMode)
        {
            if (Input.GetMouseButton(1))
            {
                isAiming = true;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                float distance = Vector3.Distance(mousePos, transform.position);
                if (distance > maxDistance)
                {
                    Vector3 direction = (mousePos - transform.position).normalized;
                    mousePos = transform.position + direction * maxDistance;
                }

                if (aimLine != null)
                {
                    aimLine.enabled = true;
                    aimLine.SetPosition(0, transform.position);
                    aimLine.SetPosition(1, mousePos);
                }
            }
            else
            {
                if (isAiming)
                {
                    isAiming = false;
                    if (aimLine != null)
                        aimLine.enabled = false;
                }
            }

            if (Input.GetMouseButtonDown(0) && isAiming && !isThrowing)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                float distance = Vector3.Distance(mousePos, transform.position);
                if (distance > maxDistance)
                {
                    Vector3 direction = (mousePos - transform.position).normalized;
                    mousePos = transform.position + direction * maxDistance;
                }

                targetPosition = mousePos;
                isThrowing = true;
                isAiming = false;
                if (aimLine != null)
                    aimLine.enabled = false;

                Collider2D col = GetComponent<Collider2D>();
                if (col != null) col.enabled = false;
            }
        }
        else
        {
            if (aimLine != null)
                aimLine.enabled = false;
        }

        if (isThrowing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                throwSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isThrowing = false;
                Collider2D col = GetComponent<Collider2D>();
                if (col != null) col.enabled = true;
            }
        }
    }
}