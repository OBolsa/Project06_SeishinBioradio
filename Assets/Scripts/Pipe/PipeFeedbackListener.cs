using UnityEngine;
using UnityEngine.Events;

public class PipeFeedbackListener : MonoBehaviour
{
    [SerializeField] private PipeChannel m_Channel;
    [SerializeField] private PipeTrail m_Trail;
    [SerializeField] private UnityEvent m_OnPipeEnter;
    [SerializeField] private UnityEvent m_OnPipeExit;

    private void OnEnable()
    {
        m_Channel.OnEnterPipeTrail += PipeEnter;
        m_Channel.OnExitPipeTrail += PipeExit;
    }

    private void OnDestroy()
    {
        m_Channel.OnEnterPipeTrail -= PipeEnter;
        m_Channel.OnExitPipeTrail -= PipeExit;
    }

    public void PipeEnter(int trailID)
    {
        if (trailID != m_Trail.PipeTrailID)
            return;

        m_OnPipeEnter?.Invoke();
    }

    public void PipeExit(int trailID)
    {
        if (trailID != m_Trail.PipeTrailID)
            return;

        m_OnPipeExit?.Invoke();
    }
}