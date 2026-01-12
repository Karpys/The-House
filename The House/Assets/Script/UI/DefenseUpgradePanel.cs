namespace Script.UI
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class DefenseUpgradePanel : MonoBehaviour
    {
        [SerializeField] private Image m_DefenseIcon = null;
        [SerializeField] private TMP_Text m_DefenseName = null;
        
        public void Initialize(BaseDefense defense)
        {
            m_DefenseIcon.sprite = defense.DefenseInfo.Sprite;
            m_DefenseName.text = defense.DefenseInfo.DefenseName;
        }
    }
}