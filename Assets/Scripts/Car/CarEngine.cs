using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarInfo))]
[RequireComponent(typeof(AudioSource))]

public class CarEngine : MonoBehaviour
{
    CarInfo carInfo;
    AudioSource audioSource;

    [SerializeField] float modifier;

    [SerializeField]
    private AnimationCurve soundPitch = new AnimationCurve(
        new Keyframe(-10, 1),
        new Keyframe(0, 1.3f),
        new Keyframe(65, 3.8f)
        );

    // Start is called before the first frame update
    void Start()
    {
        carInfo = GetComponent<CarInfo>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (carInfo.Speed * 3.6f > 0)
        {
            audioSource.pitch = soundPitch.Evaluate(Mathf.Abs((carInfo.Speed * 3.6f + 5) - (carInfo.GearBox.GearMinSpeed - 10)));
        }
        else
        {
            audioSource.pitch = soundPitch.Evaluate(Mathf.Abs((carInfo.Speed * 2.0f)));
        }

        print(carInfo.Speed);
        print(carInfo.GearBox.GearMinSpeed);
    }
}
