using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TweenType
{
    Move,
    Rotate
}

public class SimpleTween : MonoBehaviour
{
    
}

[System.Serializable]
public class TweenConfig
{
    public TweenType Type;


    [SerializeField] protected float m_MovementSpeed;
}