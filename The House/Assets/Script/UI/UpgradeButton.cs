namespace Script.UI
{
    using KarpysDev.KarpysUtils.UI;
    using UnityEngine;

    public class UpgradeButton : ButtonPointer
    {
        [SerializeField] private UpgradeUIHolder m_UIHolder = null;
        public override void Trigger()
        {
            m_UIHolder.TryUpgrade();
        }
    }
}