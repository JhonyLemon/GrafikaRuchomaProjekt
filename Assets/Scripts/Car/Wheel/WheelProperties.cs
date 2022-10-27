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
    private float SideWaysStiffness;

    public bool IsSteeringWheel { get => isSteeringWheel; }
    public bool IsThrottleWheel { get => isThrottleWheel; }
    public bool IsBrakingWheel { get => isBrakingWheel; }
    public WheelCollider WheelCollider { get => wheelCollider; }
    public GameObject Wheel { get => wheel; }
    public float SideWaysStiffness1 { get => SideWaysStiffness; set => SideWaysStiffness = value; }

    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

}
