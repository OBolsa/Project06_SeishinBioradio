using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    Hungry,
    Thristy,
    Radiation
}

[System.Serializable]
public class Condition
{
    protected string m_Name;
    protected float m_Stat;
    protected float m_MaxStat;

    private ConditionChannel m_Channel;

    public string Name => m_Name;
    public float Stat => m_Stat;
    public float MaxStat => m_MaxStat;

    public Condition(float maxStat)
    {
        m_MaxStat = maxStat;
    }

    public void StartStat(ConditionChannel channel, float initialStat)
    {
        m_Channel = channel;
        SetStat(initialStat);
    }

    public void AddStat(float stat)
    {
        SetStat(m_Stat + stat);

        if(Stat > MaxStat)
        {
            SetStat(MaxStat);
        }
    }

    public void LostStat(float stat)
    {
        SetStat(m_Stat - stat);

        if(Stat < 0)
        {
            SetStat(0);
        }
    }

    public void SetStat(float stat)
    {
        m_Stat = stat;
        m_Channel.RaiseConditionChange(this);
    }
}
