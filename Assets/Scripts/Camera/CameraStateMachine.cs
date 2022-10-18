using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine : MonoBehaviour
{
    [SerializeField] private CameraChannel m_Channel;
    public static CameraStateMachine Instance { get; private set; }
    private string m_CurrentCamera;
    private string m_PreviousCamera;
    public string CurrentCamera => m_CurrentCamera;
    public string PreviousCamera => m_PreviousCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        m_Channel.OnCameraInOut += ChangeCameraInOutTwoSeconds;
    }

    private void OnDestroy()
    {
        m_Channel.OnCameraInOut -= ChangeCameraInOutTwoSeconds;
    }

    private void Start()
    {
        StartCamera("Gameplay01 Cam");
    }

    private void StartCamera(string startCamera)
    {
        m_PreviousCamera = startCamera;
        m_CurrentCamera = startCamera;
    }

    public void UpdateCamera(string newCamera)
    {
        if (CurrentCamera == newCamera)
            return;

        m_PreviousCamera = CurrentCamera;
        m_CurrentCamera = newCamera;
    }

    public void ChangeCameraInOutTwoSeconds(string cameraName)
    {
        StartCoroutine(CameraInOut(cameraName, 2f));
    }

    private IEnumerator CameraInOut(string cameraName, float time)
    {
        m_Channel.OnChangeCamera?.Invoke(cameraName);
        Instance.UpdateCamera(cameraName);

        yield return new WaitForSeconds(time);

        m_Channel.OnChangeCamera?.Invoke(Instance.PreviousCamera);
        Instance.UpdateCamera(Instance.PreviousCamera);
    }
}
