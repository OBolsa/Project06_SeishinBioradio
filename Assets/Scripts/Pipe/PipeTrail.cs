using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTrail : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PipeChannel m_Channel;
    [SerializeField] private Transform m_MiddlePoint;

    [Header("Configs")]
    [SerializeField] private int m_PipeTrailID;
    [SerializeField] private float m_Speed;
    public int PipeTrailID => m_PipeTrailID;

    [Header("Way")]
    [SerializeField] private Transform m_PipeTrigger;
    [SerializeField] private Transform m_PipeExit;

    private Pipe[] m_Pipes;

    private Vector3 start;
    private Vector3 target;

    private int currentPipe;

    private float counter;
    private bool move;

    private void OnEnable()
    {
        m_Channel.OnEnterPipeTrail += StartMove;
    }

    private void OnDestroy()
    {
        m_Channel.OnEnterPipeTrail -= StartMove;
    }

    private void Start()
    {
        GetPipes();
        SetWaysStartPositions();
    }

    private void Update()
    {
        if (move)
        {
            counter += Time.deltaTime * RealSpeed();

            m_MiddlePoint.position = Vector3.Lerp(start, target, counter);

            if(counter >= 1)
            {
                counter = 0;

                if(currentPipe + 1 < m_Pipes.Length)
                {
                    currentPipe += 1;

                    SetTarget();
                }
                else
                {
                    move = false;
                    m_Channel.RaiseExitPipeTrail(m_PipeTrailID);
                }
            }
        }
    }

    private float RealSpeed()
    {
        return 2f / Mathf.Abs(Vector3.Distance(start, target)) * m_Speed;
    }

    private void GetPipes()
    {
        Pipe[] pipes = GetComponentsInChildren<Pipe>();

        m_Pipes = new Pipe[pipes.Length];

        for (int i = 0; i < pipes.Length; i++)
        {
            m_Pipes[i] = pipes[i];
        }
    }

    private void SetWaysStartPositions()
    {
        if (m_Pipes.Length <= 0)
            return;

        m_PipeTrigger.position = m_Pipes[0].EnterPoint.position;
        m_PipeExit.position = m_Pipes[m_Pipes.Length - 1].EndPoint.position + (m_Pipes[m_Pipes.Length - 1].transform.forward * -1) * 0.5f;
        m_MiddlePoint.position = m_PipeTrigger.position;
    }

    public void StartMove(int trailID)
    {
        if (trailID != m_PipeTrailID)
            return;

        currentPipe = 0;
        counter = 0;
        move = true;

        SetTarget();

        PipeManager.Instance.UpdatePipeEnter(m_Pipes[0].EnterPoint);
        PipeManager.Instance.UpdatePipeExitExitPoint(m_PipeExit);
        PipeManager.Instance.UpdatePipeExitEnterPoint(m_Pipes[m_Pipes.Length - 1].EnterPoint);
        PipeManager.Instance.UpdateMiddlePoint(m_MiddlePoint);
    }

    public void SetTarget()
    {
        if (currentPipe >= m_Pipes.Length)
            return;

        start = m_Pipes[currentPipe].EnterPoint.position;
        target = m_Pipes[currentPipe].EndPoint.position;
    }
}
