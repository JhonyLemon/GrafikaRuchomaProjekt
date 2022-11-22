using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FakeGearBox
{
    
    private FakeGear currentCopy;
    private FakeGear current;
    [SerializeField]
    private List<FakeGear> fakeGears;
    [SerializeField]
    private float maxTopSpeedForward;
    [SerializeField]
    private float maxTopSpeedBackword;

    public FakeGearBox(List<FakeGear> fakeGears, float maxTopSpeedForward, float maxTopSpeedBackword)
    {
        this.current = fakeGears[0];
        this.currentCopy = new FakeGear(this.current);
        this.fakeGears = fakeGears;
        this.maxTopSpeedForward = maxTopSpeedForward;
        this.maxTopSpeedBackword = maxTopSpeedBackword;
    }

    public float GearRatio { get => currentCopy.GearRatio; }

    public float GearMinSpeed { get => currentCopy.MinSpeed; }


    public void UpdateGearBox(float speed, float input)
    {
        currentCopy.GearRatio = current.GearRatio;
        if (speed > maxTopSpeedForward)
        {
            if (input > 0)
            {
                currentCopy.GearRatio *= -1;
            }
            else if (input < 0)
            {
                currentCopy.GearRatio *= 1;
            }
            else
            {
                currentCopy.GearRatio = 0;
            }
        }
        else if (speed < maxTopSpeedBackword)
        {
            if (input > 0)
            {
                currentCopy.GearRatio *= 1;
            }
            else if (input < 0)
            {
                currentCopy.GearRatio *= -1;
            }
            else
            {
                currentCopy.GearRatio = 0;
            }
        }
        else
        {
            foreach (var gear in fakeGears)
            {
                if (speed > gear.MinSpeed && speed < gear.MaxSpeed)
                {
                    current = new FakeGear(gear);
                    currentCopy = new FakeGear(gear);
                    break;
                }
            }
        }
    }
}
