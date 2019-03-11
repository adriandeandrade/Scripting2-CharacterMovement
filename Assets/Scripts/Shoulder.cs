using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoulder : MonoBehaviour
{
    [SerializeField] private float shoulderAnimateTime;
    [SerializeField] private AnimationCurve shoulderCurve;

    bool isAnimating;

    Vector3 aimAngle = Vector3.zero;
    Vector3 restAngle = new Vector3(90f, 0f, 0f);

    public void StartShoulderAnimation(bool isAiming)
    {
        if (isAnimating) return;

        if (isAiming)
        {
            StartCoroutine(AnimateShoulder(aimAngle, shoulderAnimateTime));
        }
        else
        {
            StartCoroutine(AnimateShoulder(restAngle, shoulderAnimateTime));
        }
    }

    IEnumerator AnimateShoulder(Vector3 targetAngle, float duration)
    {
        isAnimating = true;
        float animateTime = 0f;

        while (animateTime <= duration)
        {
            animateTime += Time.deltaTime;
            float percent = Mathf.Clamp01(animateTime / duration);
            transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, targetAngle, percent);
            yield return null;
        }

        isAnimating = false;
    }

    //IEnumerator AnimateShoulder(Vector3 targetAngle, float duration)
    //{
    //    isAnimating = true;
    //    float animateTime = 0f;

    //    while (animateTime <= duration)
    //    {
    //        animateTime += Time.deltaTime;
    //        float percent = Mathf.Clamp01(animateTime / duration);
    //        float curvePercent = shoulderCurve.Evaluate(percent);
    //        transform.localEulerAngles = Vector3.SlerpUnclamped(transform.localEulerAngles, targetAngle, curvePercent);
    //        yield return null;
    //    }

    //    isAnimating = false;
    //}
}
