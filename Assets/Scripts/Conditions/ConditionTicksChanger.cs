using UnityEngine;

public class ConditionTicksChanger : MonoBehaviour
{
    [SerializeField] private ConditionNewTicksConfig m_NewHungryConfig;
    [SerializeField] private ConditionNewTicksConfig m_NewThirstyConfig;
    [SerializeField] private ConditionNewTicksConfig m_NewRadiationConfig;

    public void SetNewTicksConfigs()
    {
        ConditionSystem.Instance.HungryTicks.SetValues(m_NewHungryConfig.NewTicksPerSecond, m_NewHungryConfig.NewDecayValue, m_NewHungryConfig.NewGrouwthValue);
        ConditionSystem.Instance.ThirstyTicks.SetValues(m_NewThirstyConfig.NewTicksPerSecond, m_NewThirstyConfig.NewDecayValue, m_NewThirstyConfig.NewGrouwthValue);
        ConditionSystem.Instance.RadiationTicks.SetValues(m_NewRadiationConfig.NewTicksPerSecond, m_NewRadiationConfig.NewDecayValue, m_NewRadiationConfig.NewGrouwthValue);
    }
}

[System.Serializable]
public class ConditionNewTicksConfig
{
    [SerializeField] private float m_NewTicksPerSecond;
    [SerializeField] private float m_NewDecayValue;
    [SerializeField] private float m_NewGrouwthValue;
    public float NewTicksPerSecond => m_NewTicksPerSecond;
    public float NewDecayValue => m_NewDecayValue;
    public float NewGrouwthValue => m_NewGrouwthValue;
}