using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement ariaMovement;
    public PlayerMovement shadowMovement;
    public Camera mainCamera;

    private bool isControllingAria = true;

    void Start()
    {
        isControllingAria = true;
        UpdateControl();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // قبل از سوئیچ کردن، اگر الان کنترل دست سایه است:
            // باید آریا را به موقعیت سایه بیاوریم
            if (!isControllingAria) 
            {
                ariaMovement.transform.position = shadowMovement.transform.position;
            }

            isControllingAria = !isControllingAria;
            UpdateControl();
        }

        // وقتی کنترل دست آریاست، سایه را همیشه همراهش نگه دار (برای سوئیچ بعدی آماده باشد)
        if (isControllingAria)
        {
            shadowMovement.transform.position = ariaMovement.transform.position;
        }
    }

    void UpdateControl()
    {
        int baseMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI");
        Rigidbody2D ariaRb = ariaMovement.GetComponent<Rigidbody2D>();
        Rigidbody2D shadowRb = shadowMovement.GetComponent<Rigidbody2D>();

        if (isControllingAria)
        {
            ariaMovement.enabled = true;
            ariaRb.simulated = true;
            ariaRb.bodyType = RigidbodyType2D.Dynamic;

            shadowMovement.enabled = false;
            shadowRb.simulated = false;
            
            mainCamera.cullingMask = baseMask | (1 << LayerMask.NameToLayer("AriaLayer"));
        }
        else
        {
            ariaMovement.enabled = false;
            ariaRb.simulated = false;

            shadowMovement.enabled = true;
            shadowRb.simulated = true;
            shadowRb.bodyType = RigidbodyType2D.Dynamic;
            shadowRb.velocity = Vector2.zero;

            mainCamera.cullingMask = baseMask | (1 << LayerMask.NameToLayer("ShadowLayer")) | (1 << LayerMask.NameToLayer("ShadowWorld"));
        }
    }
}