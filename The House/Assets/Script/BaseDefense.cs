namespace Script
{
    using System.Collections.Generic;
    using Data;
    using UnityEngine;

    public abstract class BaseDefense : MonoBehaviour
    {
        [Header("Upgrades")]
        [SerializeField] private UpgradeScriptableObject[] m_UpgradeScriptableObjects = null;
        [SerializeField] private DefenseInfoScriptableObject m_DefenseInfo = null;

        protected Dictionary<UpgradeType, Upgrade> m_Upgrades = new Dictionary<UpgradeType, Upgrade>();
        protected DefenseUpgrade[] m_DefenseUpgrades = null;

        public DefenseInfoScriptableObject DefenseInfo => m_DefenseInfo;
        public Dictionary<UpgradeType, Upgrade> Upgrades => m_Upgrades;

        protected virtual void Awake()
        {
            InitializeUpgrade();
        }

        private void InitializeUpgrade()
        {
            m_DefenseUpgrades = new DefenseUpgrade[m_UpgradeScriptableObjects.Length];

            for (int i = 0; i < m_UpgradeScriptableObjects.Length; i++)
            {
                m_DefenseUpgrades[i] = new DefenseUpgrade(0, m_UpgradeScriptableObjects[i]);
            }
        }

        public DefenseUpgrade[] GetUpgrades()
        {
            return m_DefenseUpgrades;
        }

        public void AddUpgrade(Upgrade upgrade,bool reload = true)
        {
            if (m_Upgrades.ContainsKey(upgrade.UpgradeType))
            {
                //Sum upgrade
            }
            else
            {
                m_Upgrades.Add(upgrade.UpgradeType,upgrade);
            }

            if (reload)
            {
                ApplyUpgrade();
            }
                
        }

        protected virtual void ApplyUpgrade()
        {
            return;
        }
    }
}