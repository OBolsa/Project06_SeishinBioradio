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
        if (stat == 0)
            return;

        SetStat(m_Stat + stat);

        if(Stat > MaxStat)
        {
            SetStat(MaxStat);
        }
    }

    public void LostStat(float stat)
    {
        if (stat == 0)
            return;

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

    public void VerifyStat(Condition condition)
    {
        if (condition != this)
            return;

        float value = condition.Stat;

        switch (value)
        {
            case < 1:
                Debug.Log(m_Name + " Acabou");
                break;
            case < 26:
                Debug.Log(m_Name + " Esta em 25%");
                break;
            case < 51:
                Debug.Log(m_Name + " Esta em 50%");
                break;
            case < 76:
                Debug.Log(m_Name + " Esta em 75%");
                break;
        }
    }
}