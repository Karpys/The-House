namespace Script.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "TypeUpgrade", menuName = "Upgrade/Type", order = 0)]
    public class TypeUpgradeScriptableObject : UpgradeScriptableObject
    {
        [SerializeField] private UpgradeType m_UpgradeType = UpgradeType.PercentRange;
        
        public string GetValue()
        {
            return "10";
        }

        public override Upgrade GetUpgrade()
        {
            return new Upgrade(m_UpgradeType, GetValue());
        }
    }
}