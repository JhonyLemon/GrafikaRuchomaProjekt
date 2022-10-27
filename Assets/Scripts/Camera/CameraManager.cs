using System;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class CameraManager : MonoBehaviour
{
    [HideInInspector]
    private CameraController controller;

    [Header("General")]
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    [Range(0, 1)]
    private int cameraMode = 0;

    private int maxCameraMode = 2;

    [Header("Third Person View")]

    [SerializeField]
    private Vector3 thirdPersonViewLocation;
    [SerializeField]
    private Vector3 locationToLookAt = new Vector3(0,2,0);
    [SerializeField]
    private float fovThirdPersonView = 60f;

    [SerializeField]
    private float dampening = 2f;

    [Header("Hood View")]

    [SerializeField]
    private Vector3 hoodViewLocation;
    [SerializeField]
    private float fovHoodView = 90f;

    private void Start()
    {
        controller = GetComponent<CameraController>();
        controller.CameraModeChanged += cameraModeChanged;
    }

    void Update()
    {
        switch (cameraMode)
        {
            case 1:
                camera.transform.position =
                    transform.position + transform.TransformDirection(hoodViewLocation);
                camera.transform.rotation = transform.rotation;
                Camera.main.fieldOfView = fovHoodView;
                break;
            default:
                camera.transform.position = Vector3.Lerp(
                    camera.transform.position,
                    transform.position + transform.TransformDirection(thirdPersonViewLocation),
                    dampening * Time.deltaTime
                    );
                camera.transform.LookAt(
                    transform.position+transform.up*locationToLookAt.y+transform.forward * locationToLookAt.z+transform.right * locationToLookAt.x
                    );
                Camera.main.fieldOfView = fovThirdPersonView;
                break;
        }


    }

    private void cameraModeChanged(object sender, int e)
    {
        cameraMode = Math.Abs((cameraMode + e) % maxCameraMode);
    }

    private void OnDestroy()
    {
        controller.CameraModeChanged-=cameraModeChanged;
    }
}
