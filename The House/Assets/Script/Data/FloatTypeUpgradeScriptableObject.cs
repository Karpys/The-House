namespace Script.Data
{
    using System.Globalization;
    using UnityEngine;

    [CreateAssetMenu(fileName = "FloatTypeUpgrade", menuName = "Upgrade/FloatType", order = 0)]
    public class FloatTypeUpgradeScriptableObject : UpgradeScriptableObject
    {
        [SerializeField] private UpgradeType m_UpgradeType = UpgradeType.PercentRange;
        [SerializeField] private AnimationCurve m_ValueCurve =  AnimationCurve.Linear(0,0,1,1); 
        [SerializeField] private Vector2 m_MinMaxValue = Vector2.zero;
        
        public string GetValue(int level)
        {
            float value = Mathf.Lerp(m_MinMaxValue.x,m_MinMaxValue.y,m_ValueCurve.Evaluate((float)level/m_MaxLevel));
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public override Upgrade GetUpgrade(int level)
        {
            return new Upgrade(m_UpgradeType, GetValue(level));
        }
    }
}