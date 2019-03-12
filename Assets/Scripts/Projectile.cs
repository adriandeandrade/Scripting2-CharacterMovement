using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask damageableMask;

    float speed = 10f;
    float damage = 1f;

    float destroyAfterTime = 3f;
    float skinWidth = .1f;

    public float Speed { get { return speed; } set { speed = value; } }

    private void Start()
    {
        Destroy(gameObject, destroyAfterTime); // Eventually destroy even if we dont collider.
    }


    private void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    private void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance + skinWidth, damageableMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider);
        }
    }

    private void OnHitObject(Collider other)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if(damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
