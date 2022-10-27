using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightProperties : MonoBehaviour
{
    private new Light light;
    private float lightIntensity;
    [SerializeField]
    private bool isOn = false;
    [SerializeField]
    private bool isFrontLight;
    [SerializeField]
    private bool isBackLight;

    void Start()
    {
        light = GetComponent<Light>();
        if (light != null)
        {
            lightIntensity = light.intensity;
        }
        IsOn = isOn;
    }    
    public bool IsFrontLight
    {
        get { return isFrontLight; }
    }

    public bool IsBackLight
    {
        get { return isBackLight; }
    }
    public bool IsOn
    {
        get { return isOn; }
        set { 
            isOn = value;
            light.intensity = isOn ? lightIntensity : 0;
        }
    }
}
