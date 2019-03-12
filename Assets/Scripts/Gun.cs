using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    [SerializeField] private float cooldown;
    [SerializeField] private float reloadTime;
    [SerializeField] private float bulletsInMag;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletForce;
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private Animator shoulderAnimator;

    float nextShotTime;
    float remainingBulletsInMag;

    bool isReloading;

    Camera cam;
    ShootingController shootingController;

    private void Awake()
    {
        cam = Camera.main;
        shootingController = GetComponentInParent<ShootingController>();
    }

    private void Start()
    {
        nextShotTime = cooldown;
        remainingBulletsInMag = bulletsInMag;
        isReloading = false;
    }

    private void LateUpdate()
    {
        if (!isReloading && remainingBulletsInMag == 0)
        {
            Reload();
        }
    }

    public void Shoot()
    {
        if (!isReloading && Time.time > nextShotTime && remainingBulletsInMag > 0)
        {
            shoulderAnimator.SetTrigger("shoot");
            remainingBulletsInMag--;
            nextShotTime = Time.time + cooldown;

            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
            {
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(bulletDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * bulletForce, ForceMode.Impulse);
                }
            }
            

            Debug.Log("Fired Gun");
        }
    }

    private void Reload()
    {
        if (!isReloading && remainingBulletsInMag != bulletsInMag)
        {
            StartCoroutine(ReloadTime());
        }
    }

    IEnumerator ReloadTime()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.2f);

        float reloadSpeed = 1f / reloadTime;
        float percent = 0f;

        while (percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            yield return null;
        }

        isReloading = false;
        remainingBulletsInMag = bulletsInMag;
    }
}
