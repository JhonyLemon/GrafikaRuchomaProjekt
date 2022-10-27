using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CameraController : MonoBehaviour
{
    [HideInInspector]
    private PlayerInput cameraControlls;
    [HideInInspector]
    private InputAction cameraModeAction;

    private Dictionary<string, int> cameraCycleKeys = new Dictionary<string, int>();
    private event EventHandler<int> cameraModeChanged;

    public EventHandler<int> CameraModeChanged
    {
        get { return cameraModeChanged; }
        set { cameraModeChanged = value; }
    }

    void Start()
    {
        cameraControlls = GetComponent<PlayerInput>();
        cameraModeAction = cameraControlls.actions["Camera Mode"];
        cameraCycleKeys.Add("/" + System.Text.RegularExpressions.Regex.Replace(cameraModeAction.bindings[0].effectivePath, "[<>]", ""), 1);
        cameraCycleKeys.Add("/" + System.Text.RegularExpressions.Regex.Replace(cameraModeAction.bindings[1].effectivePath, "[<>]", ""), -1);
        cameraModeAction.started += CameraModeAction_started;
    }

    private void OnDestroy()
    {
        cameraModeAction.started -= CameraModeAction_started;
    }

    private void CameraModeAction_started(InputAction.CallbackContext obj)
    {
        foreach (var binding in cameraCycleKeys)
        {
            if (binding.Key.Equals(obj.control.path))
            {
                onCameraModeChanged(binding.Value);
            }
        }
    }

    protected virtual void onCameraModeChanged(int modeChange)
    {
        cameraModeChanged?.Invoke(this, modeChange);
    }
}
