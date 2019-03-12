using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    [SerializeField] private float cooldown;
    [SerializeField] private float bulletsInMag;
    [SerializeField] private KeyCode shootKey;

    float nextShotTime;
    float remainingBulletsInMag;

    bool isReloading;

    private void Start()
    {
        nextShotTime = cooldown;
        remainingBulletsInMag = bulletsInMag;
        isReloading = false;
    }

    private void Update()
    {
        
    }

    public void Shoot()
    {
        if (!isReloading && Time.time > nextShotTime && remainingBulletsInMag > 0)
        {
            remainingBulletsInMag--;
            nextShotTime = Time.time + cooldown;
            Debug.Log("Shot gun");
        }
    }
}
