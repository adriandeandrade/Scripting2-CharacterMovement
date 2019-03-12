using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private KeyCode shootModeToggleKey;
    [SerializeField] private KeyCode shootKey;

    bool isInShootMode;

    Gun gun;
    Animator anim;
    CameraController cameraController;
    Crosshair crosshair;

    public bool IsInShootMode { get { return isInShootMode; } set { isInShootMode = value; } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();
        crosshair = FindObjectOfType<Crosshair>();
        gun = GetComponentInChildren<Gun>();
    }

    private void Start()
    {
        isInShootMode = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(shootModeToggleKey))
        {
            ToggleShootMode();
        }

        if (Input.GetKeyDown(shootKey) && isInShootMode)
        {
            gun.Shoot();
        }
    }

    private void ToggleShootMode()
    {
        isInShootMode = !isInShootMode;
        StartShoulderAnimation(isInShootMode);
    }

    private void StartShoulderAnimation(bool isAiming)
    {
        if (isAiming)
        {
            anim.SetTrigger("aim");
            crosshair.FadeOrFadeOutCrosshair(isAiming);
            cameraController.NewFov = 40f;
        }
        else
        {
            anim.SetTrigger("rest");
            crosshair.FadeOrFadeOutCrosshair(isAiming);
            cameraController.NewFov = 60f;
        }
    }
}
