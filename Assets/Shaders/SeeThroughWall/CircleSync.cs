using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");

    [SerializeField] private Material m_WallMat;
    [SerializeField] private LayerMask m_Mask;
    private Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    private void Update()
    {
        if(m_Camera != Camera.main)
            m_Camera = Camera.main;

        var dir = m_Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        if(Physics.Raycast(transform.position, dir.normalized, Mathf.Infinity, m_Mask))
        {
            m_WallMat.SetFloat(SizeID, 1.5f);
        }
        else
        {
            m_WallMat.SetFloat(SizeID, 0);
        }

        var view = m_Camera.WorldToViewportPoint(transform.position);
        m_WallMat.SetVector(PosID, view);
    }
}
