using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuTabListenner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MenuChannel m_Channel;
    [SerializeField] private MenuTabState m_State;

    [Header("Objects")]
    [SerializeField] private GameObject[] m_Children;

    private void Awake()
    {
        m_Channel.OnTabChange += UpdateTabState;
    }

    private void OnDestroy()
    {
        m_Channel.OnTabChange -= UpdateTabState;
    }

    private void Start()
    {
        SetChildrenActive(false);
    }

    private void UpdateTabState(string state)
    {
        if(state == m_State.ToString())
        {
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