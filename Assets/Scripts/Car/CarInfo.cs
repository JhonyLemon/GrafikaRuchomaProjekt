using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarInfo : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private float speed = 0;

    [Header("General")]

    [SerializeField]
    private float brakePower = 200f;

    [SerializeField]
    private float handBrakePower = 200f;

    [SerializeField]
    private float throttlePower = 4000f;

    [SerializeField]
    private AnimationCurve steerAngle = new AnimationCurve(
        new Keyframe(0,40),
        new Keyframe(200, 10),
        new Keyframe(100, 20)
        );

    [SerializeField]
    private FakeGearBox fakeGearBox = new FakeGearBox(
        new List<FakeGear>
        {
            { new FakeGear("R",1f,-50,-0.5f) },
            { new FakeGear("0",1f,-0.5f,0.5f) },
            { new FakeGear("1",8f,0.5f,40) },
            { new FakeGear("2",6f,40,85) },
            { new FakeGear("3",5.5f,85,135) },
            { new FakeGear("4",4f,135,180) },
            { new FakeGear("5",2.5f,180,210) },
            { new FakeGear("6",1.25f,210,230) },
        },
        230f,
        -30f
        );

    public float BrakePower { get => brakePower; }
    public float HandBrakePower { get => handBrakePower; }
    public float ThrottlePower { get => throttlePower; }
    public float TurnAngle { get => steerAngle.Evaluate(Mathf.Abs(SpeedKmPerH)); }
    public float Speed { get => speed; }
    public float SpeedKmPerH { get => Mathf.Round(speed * 3.6f); }
    public FakeGearBox GearBox { get => fakeGearBox; }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void CalculateSpeed()
    {
        speed = transform.InverseTransformVector(rigidbody.velocity).z;
    }
}
