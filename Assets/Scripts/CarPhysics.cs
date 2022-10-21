using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody),typeof(VehicleController))]
public class CarPhysics : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    [SerializeField]
    private List<WheelCollider> accelerationWheels;
    [SerializeField]
    private List<WheelCollider> steeringWheels;
    private List<WheelCollider> wheels;
    public Transform centerOfMass;
    private VehicleController controller;

    [SerializeField] 
    private float brakeForce = 1500.0f;

    [SerializeField]
    [Range(0f, 50f)]
    private float steerAngle = 30f;

    public float SteerAngle
    {
        get { return steerAngle; }
        set { steerAngle = Mathf.Clamp(value, 0.0f, 50.0f); }
    }

    [SerializeField]
    [Range(0.001f, 1.0f)]
    private float steerSpeed = 0.2f;
    public float SteerSpeed
    {
        get { return steerSpeed; }
        set { steerSpeed = Mathf.Clamp(value, 0.001f, 1.0f); }
    }
    [Range(2, 16)]
    [SerializeField] float diffGearing = 4.0f;
    public float DiffGearing
    {
        get { return diffGearing; }
        set { diffGearing = value; }
    }

    [Range(0.0f, 2f)]
    [SerializeField]
    private float driftIntensity = 1f;
    public float DriftIntensity
    {
        get => driftIntensity;
        set => driftIntensity = Mathf.Clamp(value, 0.0f, 2.0f);
    }

    [Range(0.5f, 10f)]
    [SerializeField]
    private float downforce = 1.0f;

    public float Downforce
    {
        get => downforce;
        set => downforce = Mathf.Clamp(value, 0, 5);
    }
    [SerializeField]
    private AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);
    [Tooltip("x: km/h y: torque")]
    [SerializeField]
    private AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

    public float Speed
    {
        get{ return speed; }
    }

    public float Steering
    {
        get
        {
            return turnInputCurve.Evaluate(controller.Steering) * SteerAngle;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<VehicleController>();

        if (rb != null && centerOfMass != null)
        {
            rb.centerOfMass = centerOfMass.localPosition;
        }

        wheels = GetComponentsInChildren<WheelCollider>().ToList();

        // Set the motor torque to a non null value because 0 means the wheels won't turn no matter what
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = 0.0001f;
        }
    }

    void FixedUpdate()
    {
        speed = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) * 3.6f;

        // Direction
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, Steering, SteerSpeed);
        }
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = 0.0001f;
            wheel.brakeTorque = 0;
        }
        if (controller.HandBrake)
        {
            foreach (WheelCollider wheel in wheels)
            {
                // Don't zero out this value or the wheel completly lock up
                wheel.motorTorque = 0.0001f;
                wheel.brakeTorque = brakeForce;
            }
        }
        else if (controller.Throttle != 0 && (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(controller.Throttle)))
        {
            foreach (WheelCollider wheel in accelerationWheels)
            {
                wheel.motorTorque = controller.Throttle * motorTorque.Evaluate(speed) * diffGearing / accelerationWheels.Count;
            }
        }
        else if (controller.Throttle != 0)
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.brakeTorque = Mathf.Abs(controller.Throttle) * brakeForce;
            }
        }


        rb.AddForce(-transform.up * speed * downforce);

    }

    void OnGUI()
    {
        if (Application.isEditor)  // or check the app debug flag
        {
            GUI.Label(new Rect(0,0,100,100), "Speed: "+speed);

        }
    }

}
