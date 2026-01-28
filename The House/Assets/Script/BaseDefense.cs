namespace Script
{
    using System;
    using System.Collections.Generic;
    using Data;
    using UnityEngine;

    public abstract class BaseDefense : MonoBehaviour
    {
        [Header("Upgrades")]
        [SerializeField] private UpgradeScriptableObject[] m_UpgradeScriptableObjects = null;
        [SerializeField] private DefenseInfoScriptableObject m_DefenseInfo = null;

        protected List<Upgrade> m_Upgrades = new List<Upgrade>();
        protected Dictionary<string, Func<string>> m_DefenseValues = new Dictionary<string, Func<string>>();
        protected DefenseUpgrade[] m_DefenseUpgrades = null;

        public DefenseInfoScriptableObject DefenseInfo => m_DefenseInfo;
        public List<Upgrade> Upgrades => m_Upgrades;

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

        public void AddUpgrade(Upgrade upgrade)
        {
            m_Upgrades.Add(upgrade);
        }

        protected virtual void ApplyUpgrade()
        {
            return;
        }

        public string GetValue(string defenseValueName)
        {
            if (m_DefenseValues.ContainsKey(defenseValueName))
            {
                return m_DefenseValues[defenseValueName].Invoke();
            }

            return "Not implemented";
        }

        public void Reload()
        {
            ApplyUpgrade();
        }
    }
}