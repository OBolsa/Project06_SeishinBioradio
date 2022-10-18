using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuListener : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MenuChannel m_Channel;
    [SerializeField] private MenuState m_State;

    [Header("Objects")]
    [SerializeField] private GameObject[] m_Children;

    private void Awake()
    {
        m_Channel.OnMenuChange += UpdateMenuState;
    }

    private void OnDestroy()
    {
        m_Channel.OnMenuChange -= UpdateMenuState;
    }

    private void Start()
    {
        SetChildrenActive(false);
    }

    private void UpdateMenuState(string state)
    {
        if(state == m_State.ToString())
        {
            GameManager.Instance.SetCursorVisible(true);
            SetChildrenActive(true);
        }
        else
        {
            SetChildrenActive(false);
        }
    }

    private void SetChildrenActiveFade(bool condition, float fade)
    {
        foreach(GameObject item in m_Children)
        {
            item.SetActive(condition);
        }
    }

    private void SetChildrenActive(bool condition)
    {
        foreach (GameObject item in m_Children)
        {
            item.SetActive(condition);
        }
    }
}