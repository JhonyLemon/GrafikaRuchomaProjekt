using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private float throtleInputSensitivity;
    [SerializeField]
    private float steeringInputSensitivity;

    private PlayerInput vehicleControls;
    [HideInInspector]
    private InputAction handBrakeAction;
    [HideInInspector]
    private InputAction moveAction;
    private float throttleLimit = 0;
    private float steeringLimit = 0;

    private float throttle=0;
    private float steering=0;

    private float ThrottleLimit
    {
        get { return moveAction.ReadValue<Vector2>().y; }
    }
    private float SteeringLimit
    {
        get { return moveAction.ReadValue<Vector2>().x; }
    }
    public float Throttle
    {
        get { return throttle; }
    }
    public float Steering
    {
        get { return steering; }
    }

    public bool HandBrake
    {
        get { return handBrakeAction.ReadValue<float>()==1f; }
    }

    private void Start()
    {
        vehicleControls = GetComponent<PlayerInput>();
        moveAction = vehicleControls.actions["Move"];
        handBrakeAction = vehicleControls.actions["HandBrake"];
    }

    private void FixedUpdate()
    {
        throttleLimit = ThrottleLimit;
        steeringLimit = SteeringLimit;
        updateThrottle();
        updateSteering();
    }

    private void updateThrottle()
    {
        if(throttle > throttleLimit)
        {
            if(throttle- throtleInputSensitivity < throttleLimit)
            {
                throttle = throttleLimit;
            }
            else
            {
                throttle -= throtleInputSensitivity;
            }
        }
        else if(throttle< throttleLimit)
        {
            if (throttle + throtleInputSensitivity > throttleLimit)
            {
                throttle = throttleLimit;
            }
            else
            {
                throttle += throtleInputSensitivity;
            }
        }
    }
    private void updateSteering()
    {
        if (steering > steeringLimit)
        {
            if (steering - steeringInputSensitivity < steeringLimit)
            {
                steering = steeringLimit;
            }
            else
            {
                steering -= steeringInputSensitivity;
            }
        }
        else if (steering < steeringLimit)
        {
            if (steering + steeringInputSensitivity > steeringLimit)
            {
                steering = steeringLimit;
            }
            else
            {
                steering += steeringInputSensitivity;
            }
        }
    }

}
