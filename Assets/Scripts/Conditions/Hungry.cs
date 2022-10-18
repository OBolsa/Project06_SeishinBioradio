[System.Serializable]
public class Hungry : Condition
{
    public Hungry(string name, float maxStat) : base(maxStat)
    {
        m_Name = name;
    }
}