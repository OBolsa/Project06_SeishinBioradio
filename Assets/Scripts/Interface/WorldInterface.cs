using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInterface : MonoBehaviour
{
    [SerializeField] private Transform m_LookAt;
    [SerializeField] private Vector3 m_Offset;
    private RectTransform _Transform;

    private void Awake()
    {
        _Transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(m_LookAt.position);
        //Debug.Log(pos);

        if(_Transform.position != pos)
            _Transform.position = pos;
    }
}