using UnityEngine;

public class FlowStateMachine : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_Channel;
    [SerializeField]
    private FlowState m_StartupState;
    [SerializeField]
    private FlowState[] m_PossibleStates;

    private FlowState m_CurrentState;
    public FlowState CurrentState => m_CurrentState;

    private FlowState m_PreviousState;
    public FlowState PreviousState => m_PreviousState;

    private static FlowStateMachine ms_Instance;
    public static FlowStateMachine Instance => ms_Instance;
    public FlowChannel Channel => m_Channel;

    private void Awake()
    {
        ms_Instance = this;

        m_Channel.OnFlowStateRequested += SetFlowState;
    }

    private void Start()
    {
        SetFlowState(m_StartupState);
    }

    private void OnDestroy()
    {
        m_Channel.OnFlowStateRequested -= SetFlowState;

        ms_Instance = null;
    }

    private void SetFlowState(FlowState state)
    {
        if (m_CurrentState != state)
        {
            m_PreviousState = m_CurrentState;
            m_CurrentState = state;
            m_Channel.RaiseFlowStateChanged(m_CurrentState);
        }
    }

    public FlowState StateByName(string stateName)
    {
        FlowState state = null;

        for (int i = 0; i < m_PossibleStates.Length; i++)
        {
            if (stateName == m_PossibleStates[i].name)
            {
                state = m_PossibleStates[i];
                break;
            }
        }
        return state;
    }
}