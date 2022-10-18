using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateSwitcher : MonoBehaviour
{
    [SerializeField] private CameraChannel m_Channel;
    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_Channel.OnChangeCamera += SwitchCamera;
    }

    private void OnDestroy()
    {
        m_Channel.OnChangeCamera -= SwitchCamera;
    }

    public void SwitchCamera(string newCamera)
    {
        Anim.Play(newCamera);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Anim.Play("Door01 Cam");
        }
    }
}