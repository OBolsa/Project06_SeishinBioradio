using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrajetory : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float m_MiddlePoint;
    [SerializeField, Range(0, 1)] private float m_JumpHigh;
    [SerializeField, Range(1, 5)] private float m_JumpSpeed;
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private FlowState m_DefaultState;
    [SerializeField] private FlowState m_JumpingState;
    [SerializeField] private FlowChannel m_FlowChannel;
    [SerializeField] private Transform m_PointC;
    private Transform m_Player;

    private float m_PlayerFeetOffset;
    private Vector3 m_PointCWithOffset;
    private Quaternion m_LookRotation;

    private Vector3 m_PointA;
    private Vector3 m_PointB;

    private Vector3 m_PointAB = new();
    private Vector3 m_PointBC = new();

    private float interpolatedTime;
    private bool startJump;

    private void Start()
    {
        m_Player = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        if (startJump)
        {
            interpolatedTime = (interpolatedTime + Time.deltaTime * m_JumpSpeed);

            m_Player.rotation = Quaternion.Slerp(m_Player.rotation, m_LookRotation, interpolatedTime * 3);

            m_PointAB = Vector3.Lerp(m_PointA, m_PointB, interpolatedTime);
            m_PointBC = Vector3.Lerp(m_PointB, m_PointCWithOffset, interpolatedTime);

            m_Player.position = Vector3.Lerp(m_PointAB, m_PointBC, interpolatedTime * m_JumpCurve.Evaluate(interpolatedTime));

            if(interpolatedTime >= 1f)
            {
                StopJump();
            }
        }
    }

    private void SetMiddlePointInfo()
    {
        m_PointB = Vector3.Lerp(m_PointA, m_PointCWithOffset, m_PointCWithOffset.y > m_PointA.y ? m_MiddlePoint : -m_MiddlePoint);

        float hightDifference = Mathf.Abs(m_PointCWithOffset.y - m_PointA.y);

        if (hightDifference > 1f)
        {
            m_PointB.y = m_PointCWithOffset.y > m_PointA.y ? m_PointCWithOffset.y + (m_PointCWithOffset.y - m_PointA.y) * m_JumpHigh : m_PointA.y + (m_PointA.y - m_PointCWithOffset.y) * m_JumpHigh;
        }
        else
        {
            m_PointB.y = m_PointA.y + m_PlayerFeetOffset;
        }
    }

    private void SetStartPointInfo()
    {
        m_Player.GetComponent<CharacterController>().enabled = false;

        m_PlayerFeetOffset = m_Player.position.y - m_Player.GetComponent<PlayerController>().m_PlayerFeet.position.y;
        m_PointCWithOffset = m_PointC.position;
        m_PointCWithOffset.y += m_PlayerFeetOffset;
        m_PointA = m_Player.position;

        Vector3 lookPos = m_PointCWithOffset - m_PointA;
        lookPos.y = 0f;
        m_LookRotation = Quaternion.LookRotation(lookPos);
    }

    public void StartJump()
    {
        SetStartPointInfo();
        SetMiddlePointInfo();
        startJump = true;
        m_FlowChannel.OnFlowStateChanged(m_JumpingState);
        interpolatedTime = 0;
    }

    private void StopJump()
    {
        startJump = false;
        interpolatedTime = 0;
        m_FlowChannel.OnFlowStateChanged(m_DefaultState);
    }
}