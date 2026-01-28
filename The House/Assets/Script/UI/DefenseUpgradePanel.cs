namespace Script.UI
{
    using System.Collections;
    using KarpysDev.KarpysUtils.TweenCustom;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    public class DefenseUpgradePanel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Body = null;
        [SerializeField] private RectTransform m_PanelRect = null;
        [SerializeField] private ContentSizeFitter m_ContentSizeFitter = null;
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

            for (int i = defenseUpgrades.Length; i < m_UpgradeUIHolders.Length; i++)
            {
                m_UpgradeUIHolders[i].gameObject.SetActive(false);
            }

            StartCoroutine(CO_Display());
        }

        private IEnumerator CO_Display()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            Display();
        }
        
        private void Display()
        {
            m_Body.DoUIPosition(new Vector3(0, m_PanelRect.sizeDelta.y,0), 0.25f);
        }
    }
}