using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeOrFadeOutCrosshair(bool isAiming)
    {
        if (isAiming)
        {
            anim.SetTrigger("fadeIn");
        }
        else
        {
            anim.SetTrigger("fadeOut");
        }
    }
}
