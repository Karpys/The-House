namespace Script
{
    using Data;

    public class DefenseUpgrade
    {
        private int m_CurrentLevel = 0;
        private UpgradeScriptableObject m_UpgradeScriptableObject = null;

        public UpgradeScriptableObject UpgradeScriptableObject => m_UpgradeScriptableObject;
        public int CurrentLevel => m_CurrentLevel;

        public DefenseUpgrade(int currentLevel, UpgradeScriptableObject upgradeScriptableObject)
        {
            m_CurrentLevel = currentLevel;
            m_UpgradeScriptableObject = upgradeScriptableObject;
        }

        public void ApplyUpgrade(BaseDefense defense)
        {
            defense.AddUpgrade(m_UpgradeScriptableObject.GetUpgrade());
        }

        public string GetCurrentValue()
        {
            return "Current Value";
        }
    }
}