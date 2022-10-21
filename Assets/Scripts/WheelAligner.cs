using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAligner : MonoBehaviour
{
    public GameObject wheelModel;
    private WheelCollider wheelCollider;
    private int tick;
    [Tooltip("Update every x frames")]
    public int updateInterval=60;
    void Start()
    {
        tick = 0;
        wheelCollider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tick == updateInterval)
        {
            tick = 0;
            Vector3 pos = new Vector3(0, 0, 0);
            Quaternion quat = new Quaternion();
            wheelCollider.GetWorldPose(out pos, out quat);
            wheelModel.transform.rotation = quat;
        }
        else
        {
            tick++;
        }
    }
}
