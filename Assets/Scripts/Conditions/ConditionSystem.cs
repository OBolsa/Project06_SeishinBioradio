using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SaveSystem;

public class ConditionSystem : MonoBehaviour, ISaveable
{
    public static ConditionSystem Instance;

    [Header("Start Configs")]
    [SerializeField] private ConditionChannel m_Channel;
    [SerializeField] private float m_StartHungry;
    [SerializeField] private float m_StartThirsty;
    [SerializeField] private float m_StartRadiation;

    [Header("Ticks Configs")]
    [SerializeField] private ConditionTicks m_HungryTicks;
    [SerializeField] private ConditionTicks m_ThirstyTicks;
    [SerializeField] private ConditionTicks m_RadiationTicks;
    public ConditionTicks HungryTicks => m_HungryTicks;
    public ConditionTicks ThirstyTicks => m_ThirstyTicks;
    public ConditionTicks RadiationTicks => m_RadiationTicks;

    [Header("Events")]
    [SerializeField] private UnityEvent m_OnEmptyHungry;
    [SerializeField] private UnityEvent m_OnEmptyThirsty;
    [SerializeField] private UnityEvent m_OnFullRadiation;

    private Condition m_Hungry = new Hungry("Hungry", 100f);
    private Condition m_Thirsty = new Thirsty("Thirsty", 100f);
    private Condition m_Radiation = new Radiation("Radiation", 100f);
    public Condition Hungry => m_Hungry;
    public Condition Thirsty => m_Thirsty;
    public Condition Radiation => m_Radiation;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_Hungry.StartStat(m_Channel, m_StartHungry);
        m_Thirsty.StartStat(m_Channel, m_StartThirsty);
        m_Radiation.StartStat(m_Channel, m_StartRadiation);

        m_HungryTicks.SetupConfig(m_Hungry);
        m_ThirstyTicks.SetupConfig(m_Thirsty);
        m_RadiationTicks.SetupConfig(m_Radiation);
    }

    private void Update()
    {
        m_HungryTicks.UpdateCounter();
        m_ThirstyTicks.UpdateCounter();
        m_RadiationTicks.UpdateCounter();

        if (Input.GetKey(KeyCode.F1))
        {
            m_Hungry.AddStat(.1f);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            m_Thirsty.AddStat(.1f);
        }
        if (Input.GetKey(KeyCode.F3))
        {
            m_Radiation.AddStat(.01f);
        }
        if (Input.GetKey(KeyCode.F4))
        {
            m_Hungry.LostStat(.1f);
        }
        if (Input.GetKey(KeyCode.F5))
        {
            m_Thirsty.LostStat(.1f);
        }
        if (Input.GetKey(KeyCode.F6))
        {
            m_Radiation.LostStat(.1f);
        }
    }

    public object CaptureState()
    {
        return new SaveData
        {
            _Hungry = Hungry.Stat,
            _Thirsty = Thirsty.Stat,
            _Radiation = Radiation.Stat,
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        m_Hungry.SetStat(savedData._Hungry);
        m_Thirsty.SetStat(savedData._Thirsty);
        m_Radiation.SetStat(savedData._Radiation);
    }

    [System.Serializable]
    public struct SaveData
    {
        public float _Hungry;
        public float _Thirsty;
        public float _Radiation;
    }
}

[System.Serializable]
public class ConditionTicks
{
    [SerializeField] private float m_SecondsToDecay;
    [SerializeField] private float m_DecayValue;
    [SerializeField] private float m_GrowthValue;
    private Condition Condition;
    private float counter;
    private bool isCounting;

    public void SetupConfig(Condition condition)
    {
        Condition = condition;
        counter = m_SecondsToDecay;
        isCounting = true;
    }

    public void SetValues(float _sToDecay, float _decayValue, float _growthValue)
    {
        m_SecondsToDecay = _sToDecay;
        m_DecayValue = _decayValue;
        m_GrowthValue = _growthValue;
    }

    public void UpdateCounter()
    {
        if (isCounting)
        {
            counter -= Time.deltaTime;

            if(counter <= 0)
            {
                Condition.LostStat(m_DecayValue);
                Condition.AddStat(m_GrowthValue);

                counter = m_SecondsToDecay;
            }
        }
    }
}