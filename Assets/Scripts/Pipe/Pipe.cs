using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform m_EnterPoint;
    [SerializeField] private Transform m_EndPoint;
    public Transform EnterPoint => m_EnterPoint;
    public Transform EndPoint => m_EndPoint;
}