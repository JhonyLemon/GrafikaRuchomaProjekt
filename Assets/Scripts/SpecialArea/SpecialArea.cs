using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialArea : MonoBehaviour
{

    [Header("Area type")]
    [SerializeField]
    private AreaType type = AreaType.BoostPad;

    [Header("BoostPad")]
    [SerializeField]
    private float boostRatio = 0.5f;
    [SerializeField]
    private float boostMaxSpeed = 200;

    [Header("Stop zone")]
    [SerializeField]
    private float stopRatio = 0.5f;
    [SerializeField]
    private float stopMinSpeed = 5;

    public enum AreaType
    {
        None,
        BoostPad,
        StopZone
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rigidbody = other.attachedRigidbody;
        Vector3 velocity = rigidbody.velocity;
        Vector3 velocityTemp = rigidbody.transform.InverseTransformVector(velocity);
        float sign = Mathf.Sign(velocityTemp.z);
        switch (type)
        {
            case AreaType.BoostPad:

                float boostMaxSpeedMetersPerSecond = boostMaxSpeed / 3.6f;
                float boostRatioMetersPerSecond = boostRatio / 3.6f;

                float zSpeedAfterBoost = velocityTemp.z + sign * boostRatioMetersPerSecond;

                if (sign + Mathf.Sign(zSpeedAfterBoost) == 0)
                {
                    velocityTemp.z = sign * boostMaxSpeedMetersPerSecond;
                }
                else if (zSpeedAfterBoost < 0)
                {
                    if (zSpeedAfterBoost < -boostMaxSpeedMetersPerSecond)
                    {
                        velocityTemp.z = -boostMaxSpeedMetersPerSecond;
                    }
                    else
                    {
                        velocityTemp.z = zSpeedAfterBoost;
                    }
                }
                else if (zSpeedAfterBoost > 0)
                {
                    if (zSpeedAfterBoost > boostMaxSpeedMetersPerSecond)
                    {
                        velocityTemp.z = boostMaxSpeedMetersPerSecond;
                    }
                    else
                    {
                        velocityTemp.z = zSpeedAfterBoost;
                    }
                }
                rigidbody.velocity = rigidbody.transform.TransformVector(velocityTemp);
                break;
            case AreaType.StopZone:
                float stopMinSpeedMetersPerSecond = stopMinSpeed / 3.6f;
                float stopRatioMetersPerSecond = stopRatio / 3.6f;

                float zSpeedAfterStop = velocityTemp.z - sign * stopRatioMetersPerSecond;

                if (sign + Mathf.Sign(zSpeedAfterStop) == 0)
                {
                    velocityTemp.z = sign*stopMinSpeedMetersPerSecond;
                }
                else if (zSpeedAfterStop < 0)
                {
                    if (zSpeedAfterStop > -stopMinSpeedMetersPerSecond)
                    {
                        velocityTemp.z = -stopMinSpeedMetersPerSecond;
                    }
                    else
                    {
                        velocityTemp.z = zSpeedAfterStop;
                    }
                }
                else if (zSpeedAfterStop > 0)
                {
                    if (zSpeedAfterStop < stopMinSpeedMetersPerSecond)
                    {
                        velocityTemp.z = stopMinSpeedMetersPerSecond;
                    }
                    else
                    {
                        velocityTemp.z = zSpeedAfterStop;
                    }
                }
                rigidbody.velocity = rigidbody.transform.TransformVector(velocityTemp);
                break;

        }

        
    }

}
