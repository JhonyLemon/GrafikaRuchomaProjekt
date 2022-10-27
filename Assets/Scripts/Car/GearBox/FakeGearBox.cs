using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGearBox
{
    private FakeGear currentCopy;
    private FakeGear current;
    private List<FakeGear> fakeGears;
    private float maxTopSpeedForward;
    private float maxTopSpeedBackword;

    public FakeGearBox(List<FakeGear> fakeGears, float maxTopSpeedForward, float maxTopSpeedBackword)
    {
        this.current = fakeGears[0];
        this.currentCopy = new FakeGear(this.current);
        this.fakeGears = fakeGears;
        this.maxTopSpeedForward = maxTopSpeedForward;
        this.maxTopSpeedBackword = maxTopSpeedBackword;
    }

    public float GearRatio { get => currentCopy.gearRatio; }

    public void UpdateGearBox(float speed, float input)
    {
        currentCopy.gearRatio = current.gearRatio;
        if (speed > maxTopSpeedForward)
        {
            if (input > 0)
            {
                currentCopy.gearRatio *= -1;
            }
            else if (input < 0)
            {
                currentCopy.gearRatio *= 1;
            }
            else
            {
                currentCopy.gearRatio = 0;
            }
        }
        else if (speed < maxTopSpeedBackword)
        {
            if (input > 0)
            {
                currentCopy.gearRatio *= 1;
            }
            else if (input < 0)
            {
                currentCopy.gearRatio *= -1;
            }
            else
            {
                currentCopy.gearRatio = 0;
            }
        }
        else
        {
            foreach (var gear in fakeGears)
            {
                if (speed > gear.minSpeed && speed < gear.maxSpeed)
                {
                    current = new FakeGear(gear);
                    currentCopy = new FakeGear(gear);
                    break;
                }
            }
        }
    }
}
