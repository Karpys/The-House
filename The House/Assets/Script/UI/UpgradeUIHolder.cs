namespace Script.UI
{
    using TMPro;
    using UnityEngine;

    public class UpgradeUIHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_CostText = null;
        [SerializeField] private TMP_Text m_UpgradeNameText = null;
        [SerializeField] private TMP_Text m_CurrentValueText = null;

        private BaseDefense m_Defense = null;
        private DefenseUpgrade m_DefenseUpgrade = null;
        public void Initialize(BaseDefense defense, DefenseUpgrade upgrade)
        {
            m_CostText.text = upgrade.UpgradeScriptableObject.GetCost(upgrade.CurrentLevel).ToString();
            m_UpgradeNameText.text = name;
            m_CurrentValueText.text = upgrade.GetCurrentValue();
            m_Defense = defense;
            m_DefenseUpgrade = upgrade;
        }

        public void TryUpgrade()
        {
            m_DefenseUpgrade.ApplyUpgrade(m_Defense);
        }
    }
}