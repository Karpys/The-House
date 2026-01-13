namespace Script
{
    public class Upgrade
    {
        private string m_Value;
        private UpgradeType m_UpgradeType;

        public UpgradeType UpgradeType => m_UpgradeType;
        public string Value => m_Value;

        public Upgrade(UpgradeType type, string value)
        {
            m_Value = value;
            m_UpgradeType = type;
        }
        
        public void ApplyUpgrade(BaseDefense defense,string value)
        {
            switch (m_UpgradeType)
            {
                case UpgradeType.PercentRange:
                    defense.AddUpgrade(new Upgrade(UpgradeType.PercentRange, value));
                    break;
            }
        }

    }
}