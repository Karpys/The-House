namespace Script
{
    using Data;

    public class DefenseUpgrade
    {
        private int m_CurrentLevel = 0;
        private UpgradeScriptableObject m_UpgradeScriptableObject = null;
        private Upgrade m_Upgrade = null;
        public UpgradeScriptableObject UpgradeScriptableObject => m_UpgradeScriptableObject;
        public int CurrentLevel => m_CurrentLevel;

        public DefenseUpgrade(int currentLevel, UpgradeScriptableObject upgradeScriptableObject)
        {
            m_CurrentLevel = currentLevel;
            m_UpgradeScriptableObject = upgradeScriptableObject;
        }

        public void ApplyUpgrade(BaseDefense defense)
        {
            m_CurrentLevel++;

            if (m_Upgrade == null)
            {
                m_Upgrade = m_UpgradeScriptableObject.GetUpgrade(m_CurrentLevel);
                defense.AddUpgrade(m_Upgrade);
                defense.Reload();
            }
            else 
            {
                //Update upgrade
                Upgrade copy = m_UpgradeScriptableObject.GetUpgrade(m_CurrentLevel);
                m_Upgrade.SetValue(copy.Value);
                defense.Reload();
            }
        }
    }
}