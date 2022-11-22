using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarInfo))]
[RequireComponent(typeof(AudioSource))]

public class CarEngine : MonoBehaviour
{
    CarInfo carInfo;
    AudioSource audioSource;
    float soundPitch;

    [SerializeField] float modifier;

    // Start is called before the first frame update
    void Start()
    {
        carInfo = GetComponent<CarInfo>();
        audioSource = GetComponent<AudioSource>();
        soundPitch = 1f;
        modifier = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {

        soundPitch = 1f;

        if (carInfo.Speed * 3.6f > 0.3f)
        {
            soundPitch = 1f;
        }

        if (carInfo.Speed*3.6f > 10f)
        {
            soundPitch = 1.5f;
        }

        if (carInfo.Speed * 3.6f > 20f)
        {
            soundPitch = 2f;
        }

        if (carInfo.Speed * 3.6f > 30f)
        {
            soundPitch = 2.5f;
        }

        if (carInfo.Speed * 3.6f > 0)
        {
            audioSource.pitch = (((carInfo.Speed * 3.6f) - carInfo.GearBox.GearMinSpeed) / soundPitch) * modifier;
        }
        else
        {
            audioSource.pitch = ((carInfo.Speed) / soundPitch) * modifier;
        }

        print(carInfo.Speed);
        print(carInfo.GearBox.GearMinSpeed);
    }
}
