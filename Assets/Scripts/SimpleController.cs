using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class SimpleController : MonoBehaviour
{
    public List<WheelCollider> steerWheels;
    public List<WheelCollider> throttleWheels;
    public float strenghtCoefficient;
    public float maxTurnAngle;

    public InputManager inputManager;

    public GameObject centerOfMass;
    private Rigidbody rb;

    public void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    public void FixedUpdate()
    {
        foreach(var wheel in throttleWheels)
        {
            wheel.motorTorque = inputManager.throttle * Time.deltaTime * strenghtCoefficient;   
        }
        foreach(var wheel in steerWheels)
        {
            wheel.steerAngle = inputManager.steer * maxTurnAngle;
        }
    }


}
