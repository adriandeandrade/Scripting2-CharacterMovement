using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 120f;
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float sensitivity = 150f;
    [SerializeField] private float fovLerpSpeed = 0.5f;
    [SerializeField] private float followSpeed = 0.5f;
    [SerializeField] private bool doLerpTarget;
    [SerializeField] private GameObject followTarget;
    [SerializeField] private Vector3 offset;

    float rotY;
    float rotX;
    float newFov;

    bool isZoomed;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        isZoomed = false;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        newFov = cam.fieldOfView;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Mouse X"); // Get input axises
        float inputY = Input.GetAxis("Mouse Y");

        rotY += inputX * sensitivity * Time.deltaTime; // Increment / decrement rotation values on x and y axis
        rotX += -inputY * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localRotation;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newFov, Time.deltaTime / fovLerpSpeed);
    }

    private void LateUpdate()
    {
        Transform target = followTarget.transform;

        if (!isZoomed && doLerpTarget)
        {
            float step = followSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position + offset, step);
        }
        else
        {
            float step = cameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position + offset, step);
        }
    }
}
