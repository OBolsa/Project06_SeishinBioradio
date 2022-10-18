using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    [SerializeField] private MenuChannel m_Channel;
    [SerializeField] private FlowChannel m_FlowChannel;
    [SerializeField] private FlowState m_PauseState;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            m_Channel.RaiseMenu(GameManager.Instance.IsPaused? "Game" : "Pause");
        }
    }
}