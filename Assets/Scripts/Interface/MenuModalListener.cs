using UnityEngine;

public class MenuModalListener : MonoBehaviour
{
    [SerializeField] private MenuChannel m_Channel;
    [SerializeField] private string m_ModalName;

    private void Awake()
    {
        m_Channel.OnOpenModal += OpenModal;
        m_Channel.OnCloseModal += CloseModal;
    }

    private void OnDestroy()
    {
        m_Channel.OnOpenModal -= OpenModal;
        m_Channel.OnCloseModal -= CloseModal;
    }

    private void Start()
    {
        CloseModal(m_ModalName);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CloseModal(m_ModalName);
            FlowStateMachine.Instance.Channel.RaiseFlowStateRequest(FlowStateMachine.Instance.PreviousState);
        }
    }

    private void OpenModal(string modal)
    {
        if(modal == m_ModalName)
        {
            gameObject.SetActive(true);
        }
    }

    private void CloseModal(string modal)
    {
        if(modal == m_ModalName)
        {
            gameObject.SetActive(false);
        }
    }
}