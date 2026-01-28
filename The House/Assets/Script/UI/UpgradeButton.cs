namespace Script.UI
{
    using KarpysDev.KarpysUtils.UI;
    using UnityEngine;

    public class UpgradeButton : ButtonPointer
    {
        [SerializeField] private UpgradeUIHolder m_UIHolder = null;
        [SerializeField] private ShadowButtonEffect m_Effect = null;
        public override void Trigger()
        {
            m_UIHolder.TryUpgrade();
            m_Effect.Effect();
        }
    }
}