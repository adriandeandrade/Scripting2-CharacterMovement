using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private KeyCode shootModeToggleKey;

    Shoulder shoulder;

    bool isInShootMode;
    public bool IsInShootMode { get { return isInShootMode; } set { isInShootMode = value; } }

    private void Start()
    {
        isInShootMode = false;
        shoulder = GetComponentInChildren<Shoulder>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(shootModeToggleKey))
        {
            ToggleShootMode();
        }
    }

    private void ToggleShootMode()
    {
        isInShootMode = !isInShootMode;
        shoulder.StartShoulderAnimation(isInShootMode);
    }
}
