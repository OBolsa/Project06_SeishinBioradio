[System.Serializable]
public class Radiation : Condition
{
    public Radiation(string name, float maxStat) : base(maxStat)
    {
        m_Name = name;
    }
}