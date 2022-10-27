using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FakeGear
{
    [SerializeField]
    private string gearIndex;
    [SerializeField]
    private float gearRatio;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;

    public string GearIndex { get => gearIndex; set => gearIndex = value; }
    public float GearRatio { get => gearRatio; set => gearRatio = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }

    public FakeGear(string gearIndex, float gearRatio, float minSpeed, float maxSpeed)
    {
        this.gearIndex = gearIndex;
        this.gearRatio = gearRatio;
        this.minSpeed = minSpeed;
        this.maxSpeed = maxSpeed;
    }
    public FakeGear(FakeGear fakeGear)
    {
        this.gearRatio = fakeGear.gearRatio;
        this.minSpeed = fakeGear.minSpeed;
        this.maxSpeed = fakeGear.maxSpeed;
        this.gearIndex = fakeGear.gearIndex;
    }
}
