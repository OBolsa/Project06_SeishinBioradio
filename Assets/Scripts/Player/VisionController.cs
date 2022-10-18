using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private Camera m_VisionCamera;
    [SerializeField] private bool m_IsInVision;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeVision();

        if(m_VisionCamera.enabled != m_IsInVision)
        {
            m_MainCamera.enabled = !m_IsInVision;
            m_VisionCamera.enabled = m_IsInVision;
        }
    }

    private void ChangeVision()
    {
        m_IsInVision = !m_IsInVision;
    }
}