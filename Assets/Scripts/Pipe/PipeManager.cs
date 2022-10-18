using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    private Transform m_CurrentEnterPipePoint;
    private Transform m_CurrentExitPipeExitPoint;
    private Transform m_CurrentExitPipeEnterPoint;
    private Transform m_PipeMiddlePoint;
    public Transform CurrentEnterPipePoint => m_CurrentEnterPipePoint;
    public Transform CurrentExitPipeExitPoint => m_CurrentExitPipeExitPoint;
    public Transform CurrentExitPipeEnterPoint => m_CurrentExitPipeEnterPoint;
    public Transform PipeMiddlePoint => m_PipeMiddlePoint;

    public static PipeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMiddlePoint(Transform pipeMiddlePoint)
    {
        m_PipeMiddlePoint = pipeMiddlePoint;
    }

    public void UpdatePipeEnter(Transform pipeEnter)
    {
        m_CurrentEnterPipePoint = pipeEnter;
    }

    public void UpdatePipeExitExitPoint(Transform pipeExit)
    {
        m_CurrentExitPipeExitPoint = pipeExit;
    }

    public void UpdatePipeExitEnterPoint(Transform pipeExitEnterPoint)
    {
        m_CurrentExitPipeEnterPoint = pipeExitEnterPoint;
    }
}