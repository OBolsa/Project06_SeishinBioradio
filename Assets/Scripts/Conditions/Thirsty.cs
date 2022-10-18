[System.Serializable]
public class Thirsty : Condition
{
    public Thirsty(string name, float maxStat) : base(maxStat)
    {
        m_Name = name;
    }
}