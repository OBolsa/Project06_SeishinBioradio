using UnityEngine;
using UnityEngine.UI;

public class ConditionDisplayer : MonoBehaviour
{
    [SerializeField] private ConditionChannel m_Channel;
    [SerializeField] private string m_DisplayName;
    [SerializeField] private Image m_Display;

    private void Awake()
    {
        m_Channel.OnChangeCondition += UpdateDisplay;
    }

    private void OnDestroy()
    {
        m_Channel.OnChangeCondition -= UpdateDisplay;
    }

    public void UpdateDisplay(Condition condition)
    {
        if(condition.Name == name)
        {
            m_Display.fillAmount = condition.Stat / condition.MaxStat;
        }
    }
}
