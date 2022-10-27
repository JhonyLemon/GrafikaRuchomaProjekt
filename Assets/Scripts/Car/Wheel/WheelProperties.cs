using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class WheelProperties : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private bool isSteeringWheel;
    [SerializeField]
    private bool isThrottleWheel;
    [SerializeField]
    private bool isBrakingWheel;
    [HideInInspector]
    private WheelCollider wheelCollider;
    [SerializeField]
    private GameObject wheel;

    private WheelFrictionCurve sideWays;
    public WheelFrictionCurve sideWaysCopy;

    public bool IsSteeringWheel { get => isSteeringWheel; }
    public bool IsThrottleWheel { get => isThrottleWheel; }
    public bool IsBrakingWheel { get => isBrakingWheel; }
    public WheelCollider WheelCollider { get => wheelCollider; }
    public GameObject Wheel { get => wheel; }
    public WheelFrictionCurve SideWays { get => sideWays; }

    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        sideWays = wheelCollider.sidewaysFriction;
    }

    public void updateSideWays()
    {
        wheelCollider.sidewaysFriction = sideWays;
        sideWaysCopy = sideWays.CloneViaFakeSerialization();
    }
    public void applyNewSideWays()
    {
        wheelCollider.sidewaysFriction = sideWaysCopy;
    }

}
