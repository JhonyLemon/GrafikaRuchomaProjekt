using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGear
{
    public string gearIndex;
    public float gearRatio;
    public float minSpeed;
    public float maxSpeed;

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
