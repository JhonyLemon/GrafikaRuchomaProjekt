using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AntiRollBar : MonoBehaviour
{
    [SerializeField]
    private WheelProperties backLeftWheel;
    [SerializeField]
    private WheelProperties backRightWheel;
    private Rigidbody rb;
    [SerializeField]
    private float antiRoll = 5000f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        WheelHit hitLeft;
        WheelHit hitRight;

        float travelLeft = 1.0f;
        float travelRright = 1.0f;

        bool groundedLeft = backLeftWheel.WheelCollider.GetGroundHit(out hitLeft);
        bool groundedRight = backLeftWheel.WheelCollider.GetGroundHit(out hitRight);

        if (groundedLeft)
        {
            travelLeft = (-backLeftWheel.WheelCollider.transform.InverseTransformPoint(hitLeft.point).y
                - backLeftWheel.WheelCollider.radius) / backLeftWheel.WheelCollider.suspensionDistance;
        }

        if (groundedRight)
        {
            travelLeft = (-backRightWheel.WheelCollider.transform.InverseTransformPoint(hitRight.point).y
                - backRightWheel.WheelCollider.radius) / backRightWheel.WheelCollider.suspensionDistance;
        }

        var antiRollForce = (travelLeft - travelRright) * antiRoll;

        if (groundedLeft)
        {
            rb.AddForceAtPosition(
                backLeftWheel.WheelCollider.transform.up * -antiRollForce,
                backLeftWheel.WheelCollider.transform.position
                );
        }
        if (groundedLeft)
        {
            rb.AddForceAtPosition(
                backRightWheel.WheelCollider.transform.up * -antiRollForce,
                backRightWheel.WheelCollider.transform.position
                );
        }

    }
}
