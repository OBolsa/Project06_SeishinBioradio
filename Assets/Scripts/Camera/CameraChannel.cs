using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Camera Channel", fileName = "CameraChannel")]
public class CameraChannel : ScriptableObject
{
    public delegate void CameraChange(string cameraName);
    public CameraChange OnChangeCamera;
    public CameraChange OnCameraInOut;

    public void RaiseCameraChange(string cameraName)
    {
        if (cameraName == CameraStateMachine.Instance.CurrentCamera)
            return;

        OnChangeCamera.Invoke(cameraName);
        CameraStateMachine.Instance.UpdateCamera(cameraName);
    }

    public void RaiseChangeCameraAndReturnAfterTwoSeconds(string cameraName)
    {
        if (cameraName == CameraStateMachine.Instance.CurrentCamera)
            return;

        OnCameraInOut.Invoke(cameraName);
    }
}