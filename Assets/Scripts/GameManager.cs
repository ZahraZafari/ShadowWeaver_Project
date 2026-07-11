using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement ariaMovement;
    public PlayerMovement shadowMovement;
    public Camera mainCamera;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip transformClip; 

    public static Vector3 lastCheckpoint = Vector3.zero;
    public static bool mirrorMode = false;  
    private bool isControllingAria = true;

    void Start()
    {
        isControllingAria = true;
        UpdateControl();

        // اگه AudioSource نذاشتی، خودش میسازه
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayTransformSound();

            if (!isControllingAria)
            {
                ariaMovement.transform.position = shadowMovement.transform.position;
            }

            isControllingAria = !isControllingAria;
            UpdateControl();
        }

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

            mainCamera.cullingMask = baseMask
                | (1 << LayerMask.NameToLayer("ShadowLayer"))
                | (1 << LayerMask.NameToLayer("ShadowWorld"));
        }
    }

    public bool IsControllingAria()
    {
        return isControllingAria;
    }

    public void ToggleMirrorWorld()
    {
        mirrorMode = !mirrorMode;
        Debug.Log("Mirror Mode : " + mirrorMode);
    }

    public bool IsMirrorMode()
    {
        return mirrorMode;
    }

    void PlayTransformSound()
    {
        if (transformClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(transformClip);
        }
    }
}