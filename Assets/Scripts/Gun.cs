using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    [SerializeField] private float cooldown; // Cooldown between each shot.
    [SerializeField] private float reloadTime; // Time it takes to reload.
    [SerializeField] private float bulletsInMag; // Amount of bullets in one magazine.
    [SerializeField] private float bulletDamage; // How much damage the bullets do.
    [SerializeField] private float bulletForce; // How much force the bullet holds.
    [SerializeField] private KeyCode shootKey; // Shooting button.
    [SerializeField] private Animator anim; // Animator which does shoulder movement. (Recoil, Rest, Aim)

    float nextShotTime;
    float remainingBulletsInMag;

    bool isReloading;

    Camera cam;
    ShootingController shootingController;

    private void Awake()
    {
        cam = Camera.main;
        shootingController = GetComponentInParent<ShootingController>();
        anim = GetComponentInParent<Animator>();
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
            anim.SetTrigger("shoot");
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
