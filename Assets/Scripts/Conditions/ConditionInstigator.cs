using UnityEngine;

public class ConditionInstigator : MonoBehaviour
{
    [SerializeField] private ConditionType m_ConditionType;

    public void ChangeCondition(float value)
    {
        switch (m_ConditionType)
        {
            default:
            case ConditionType.Radiation:
                if (value < 0) ConditionSystem.Instance.Radiation.LostStat(Mathf.Abs(value));
                else ConditionSystem.Instance.Radiation.AddStat(value);
                break;
            case ConditionType.Hungry:
                if (value < 0) ConditionSystem.Instance.Hungry.LostStat(Mathf.Abs(value));
                else ConditionSystem.Instance.Hungry.AddStat(value);
                break;
            case ConditionType.Thristy:
                if (value < 0) ConditionSystem.Instance.Thirsty.LostStat(Mathf.Abs(value));
                else ConditionSystem.Instance.Thirsty.AddStat(value);
                break;
        }
    }
}