using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private KeyCode shootModeToggleKey;
    [SerializeField] private KeyCode shootKey;

    bool isInShootMode;

    Shoulder shoulder;
    Gun gun;
   
    public bool IsInShootMode { get { return isInShootMode; } set { isInShootMode = value; } }

    private void Start()
    {
        isInShootMode = false;
        shoulder = GetComponentInChildren<Shoulder>();
        gun = GetComponentInChildren<Gun>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(shootModeToggleKey))
        {
            ToggleShootMode();
        }

        if (Input.GetKeyDown(shootKey))
        {
            gun.Shoot();
        }
    }

    private void ToggleShootMode()
    {
        isInShootMode = !isInShootMode;
        shoulder.StartShoulderAnimation(isInShootMode);
    }
}
