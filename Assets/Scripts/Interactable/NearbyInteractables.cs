using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyInteractables : MonoBehaviour
{
    [SerializeField] private Transform m_InstigatorPoint;
    [SerializeField] private float m_MinimunDistance;
    private List<InteractionDisplayer> m_Interactables = new List<InteractionDisplayer>();
    private InteractionDisplayer m_PreviousInteractable;

    private void Update()
    {
        if (HasNearbyInteractables())
        {
            foreach (var item in m_Interactables)
            {
                if(item == m_PreviousInteractable)
                {
                    item.ShowIndication(false);
                    item.ShowDisplay(false);
                }

                if(item != ClosestInteractables())
                    item.ShowDisplay(false);
                else
                    ClosestInteractables().ShowDisplay(IsInDistanceToInteract(ClosestInteractables()));
            }
        }

        if (HasNearbyInteractables() && ClosestInteractables().DisplayActive && Input.GetButton("Submit"))
        {
            ShowAllDisplay(false);
            ShowAllIndication(false);

            m_PreviousInteractable = ClosestInteractables();
            ClosestInteractables().DoInteraction();
        }
    }

    private void ShowAllIndication(bool condition)
    {
        foreach (var item in m_Interactables)
        {
            item.ShowIndication(condition);
        }
    }

    private void ShowAllDisplay(bool condition)
    {
        foreach (var item in m_Interactables)
        {
            item.ShowDisplay(condition);
        }
    }

    public bool HasNearbyInteractables()
    {
        return m_Interactables.Count != 0;
    }

    public bool IsInDistanceToInteract(InteractionDisplayer displayer)
    {
        return Mathf.Abs(Vector3.Distance(m_InstigatorPoint.position, ClosestInteractables().transform.position)) <= m_MinimunDistance;
    }

    public InteractionDisplayer ClosestInteractables()
    {
        int closestIndex = 0;

        for (int i = 0; i < m_Interactables.Count; i++)
        {
            var atualClosest = Vector3.Distance(m_Interactables[closestIndex].transform.position, transform.position);
            var newCheackage = Vector3.Distance(m_Interactables[i].transform.position, transform.position);

            if (newCheackage < atualClosest)
            {
                closestIndex = i;
            }
        }

        return m_Interactables[closestIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractionDisplayer interactable = other.GetComponent<InteractionDisplayer>();

        if (interactable != null)
        {
            m_Interactables.Add(interactable);

            interactable.ShowIndication(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractionDisplayer interactable = other.GetComponent<InteractionDisplayer>();

        if (interactable != null)
        {
            m_Interactables.Remove(interactable);

            interactable.ShowIndication(false);
        }
    }
}