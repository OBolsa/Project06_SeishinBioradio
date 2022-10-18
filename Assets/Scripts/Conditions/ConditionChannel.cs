using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Condition Channel")]
public class ConditionChannel : ScriptableObject
{
    public delegate void ConditionCallback(Condition condition);
    public ConditionCallback OnChangeCondition;

    public void RaiseConditionChange(Condition condition)
    {
        OnChangeCondition?.Invoke(condition);
    }
}