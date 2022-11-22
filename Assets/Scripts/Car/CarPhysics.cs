using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody),typeof(VehicleController),typeof(CarInfo))]
public class CarPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private VehicleController controller;
    private CarInfo carInfo;
    [SerializeField]
    private List<WheelProperties> wheels = new List<WheelProperties>();
    [SerializeField]
    private Transform centerOfMass;
    [SerializeField]
    private LightingManager lighting;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<VehicleController>();
        carInfo = GetComponent<CarInfo>();
        if (rb != null && centerOfMass != null)
        {
            rb.centerOfMass = centerOfMass.localPosition;
        }
    }

    private void Update()
    {
        if (lighting != null)
        {
            lighting.ToggleFrontLights(controller.FrontLight);
        }
    }

    private void FixedUpdate()
    {
        carInfo.CalculateSpeed();
        carInfo.GearBox.UpdateGearBox(carInfo.SpeedKmPerH,controller.Throttle);
        foreach(WheelProperties wheel in wheels)
        {
            wheel.updateSideWays();
            if (wheel.IsThrottleWheel) //throttle
            {
                if (controller.Throttle > 0)
                {
                    if (carInfo.SpeedKmPerH < 0)
                    {
                        lighting.BrakeLights(true);
                        wheel.WheelCollider.brakeTorque = carInfo.BrakePower;
                        wheel.WheelCollider.motorTorque = 0f;
                    }
                    else
                    {
                        lighting.BrakeLights(false);
                        wheel.WheelCollider.motorTorque = carInfo.ThrottlePower * carInfo.GearBox.GearRatio * Time.deltaTime * controller.Throttle;
                        wheel.WheelCollider.brakeTorque = 0f;
                    }
                }
                else if(controller.Throttle < 0)
                {
                    if (carInfo.SpeedKmPerH > 0)
                    {
                        lighting.BrakeLights(true);
                        wheel.WheelCollider.brakeTorque = carInfo.BrakePower;
                        wheel.WheelCollider.motorTorque = 0f;
                    }
                    else
                    {
                        lighting.BrakeLights(false);
                        wheel.WheelCollider.motorTorque = carInfo.ThrottlePower * carInfo.GearBox.GearRatio * Time.deltaTime * controller.Throttle;
                        wheel.WheelCollider.brakeTorque = 0f;
                    }
                }
            }

            if (controller.HandBrake && wheel.IsBrakingWheel) //hand brake
            {
                wheel.WheelCollider.brakeTorque = carInfo.HandBrakePower * Time.deltaTime;
                wheel.WheelCollider.motorTorque = 0f;
                wheel.sideWaysCopy.stiffness -= 3f;
            }

            if (wheel.IsSteeringWheel) //steering
            {
                wheel.WheelCollider.steerAngle = carInfo.TurnAngle * controller.Steering;
            }
            wheel.applyNewSideWays();
            animateWheels(wheel);
        }
    }

    void OnGUI()
    {
        if (Application.isEditor)  // or check the app debug flag
        {

            GUI.Label(new Rect(0,0,300,50), "Speed: "+Mathf.Round(carInfo.Speed*3.6f)+" km/h");
            GUI.Label(new Rect(0, 20, 300, 50), "Current sideways slip divided by Extremum slip");
            GUI.Label(new Rect(0, 40, 300, 50), "LF: " + Vector3.Dot(wheels[0].WheelCollider.transform.right, rb.GetPointVelocity(wheels[0].transform.position)) / wheels[0].WheelCollider.sidewaysFriction.extremumSlip);
            GUI.Label(new Rect(0, 60, 300, 50), "LF: " + Vector3.Dot(wheels[1].WheelCollider.transform.right, rb.GetPointVelocity(wheels[1].transform.position)) / wheels[1].WheelCollider.sidewaysFriction.extremumSlip);
            GUI.Label(new Rect(0, 80, 300, 50), "LF: " + Vector3.Dot(wheels[2].WheelCollider.transform.right, rb.GetPointVelocity(wheels[2].transform.position)) / wheels[2].WheelCollider.sidewaysFriction.extremumSlip);
            GUI.Label(new Rect(0, 100, 300, 50), "LF: " + Vector3.Dot(wheels[3].WheelCollider.transform.right, rb.GetPointVelocity(wheels[3].transform.position)) / wheels[3].WheelCollider.sidewaysFriction.extremumSlip);
            GUI.Label(new Rect(0, 120, 300, 50), "RPM: " + carInfo.GearBox.GearRatio);

        }

    }

    private void animateWheels(WheelProperties wheel)
    {
        if (wheel.IsSteeringWheel)
        {
            wheel.Wheel.transform.localEulerAngles = new Vector3(0f, wheel.WheelCollider.steerAngle, 0);
        }
        wheel.Wheel.transform.Rotate(
            rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * wheel.WheelCollider.radius),
            0f,
            0f
            );
    }

}