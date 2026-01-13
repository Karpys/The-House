namespace Script.UI
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    public class DefenseUpgradePanel : MonoBehaviour
    {
        [SerializeField] private Image m_DefenseIcon = null;
        [SerializeField] private TMP_Text m_DefenseName = null;
        [SerializeField] private UpgradeUIHolder[] m_UpgradeUIHolders = null;
        
        public void Initialize(BaseDefense defense)
        {
            m_DefenseIcon.sprite = defense.DefenseInfo.Sprite;
            m_DefenseName.text = defense.DefenseInfo.DefenseName;

            DefenseUpgrade[] defenseUpgrades = defense.GetUpgrades();

            for (int i = 0; i < defenseUpgrades.Length; i++)
            {
                m_UpgradeUIHolders[i].Initialize(defense,defenseUpgrades[i]);
                m_UpgradeUIHolders[i].gameObject.SetActive(true);
            }
        }
    }
}