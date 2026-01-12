namespace Script
{
    using KarpysDev.KarpysUtils;
    using UI;
    using UnityEngine;

    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        [SerializeField] private Transform m_House = null;
        [SerializeField] private DefenseUpgradePanel m_UpgradePanel = null;

        public Transform House => m_House;

        public void DisplayPanelDefense(BaseDefense defense)
        {
            m_UpgradePanel.Initialize(defense);
        }
    }
}