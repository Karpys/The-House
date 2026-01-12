namespace Script.UI
{
    using TMPro;
    using UnityEngine;

    public class UpgradeUIHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_CostText = null;
        [SerializeField] private TMP_Text m_UpgradeNameText = null;
        [SerializeField] private TMP_Text m_CurrentValueText = null;

        public void Initialize(string cost, string name, string value)
        {
            m_CostText.text = cost;
            m_UpgradeNameText.text = name;
            m_CurrentValueText.text = value;
        }
    }
}