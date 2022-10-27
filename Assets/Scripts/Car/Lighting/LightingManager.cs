using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private List<LightProperties> lights = new List<LightProperties>();

    public List<LightProperties> Lights
    {
        get { return lights; }
    }

    public void ToggleFrontLights(bool toggle)
    {
        foreach (LightProperties light in lights)
        {
            if (light.IsFrontLight)
            {
                light.IsOn  = toggle;
            }
        }
    }
    public void BrakeLights(bool toggle)
    {
        foreach (LightProperties light in lights)
        {
            if (light.IsBackLight)
            {
                light.IsOn = toggle;
            }
        }
    }

}
