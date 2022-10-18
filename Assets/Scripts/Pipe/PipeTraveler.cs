using UnityEngine;

public class PipeTraveler : MonoBehaviour
{
    [SerializeField] private PipeChannel m_Channel;

    private void OnEnable()
    {
        m_Channel.OnEnterPipeTrail += EnterPipe;
        m_Channel.OnExitPipeTrail += ExitPipe;
    }

    private void OnDestroy()
    {
        m_Channel.OnEnterPipeTrail -= EnterPipe;
        m_Channel.OnExitPipeTrail -= ExitPipe;
    }

    public void EnterPipe(int trailID)
    {
        gameObject.SetActive(false);
        transform.position = PipeManager.Instance.CurrentEnterPipePoint.position;
        transform.parent = PipeManager.Instance.PipeMiddlePoint;
        transform.localPosition = Vector3.zero;
    }

    public void ExitPipe(int trailID)
    {
        transform.position = PipeManager.Instance.CurrentExitPipeExitPoint.position + Vector3.up;
        transform.parent = null;

        Vector3 lookPosition = PipeManager.Instance.CurrentExitPipeExitPoint.position + (PipeManager.Instance.CurrentExitPipeExitPoint.position - PipeManager.Instance.CurrentExitPipeEnterPoint.position) * 2f;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);

        gameObject.SetActive(true);
    }
}