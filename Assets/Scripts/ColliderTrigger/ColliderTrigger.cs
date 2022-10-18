using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] private string m_InteractionTag = "Player";
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_InteractionTag))
        {
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(m_InteractionTag))
            OnStay?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_InteractionTag))
            OnExit?.Invoke();
    }
}