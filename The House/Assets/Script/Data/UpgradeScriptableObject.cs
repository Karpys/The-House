namespace Script.Data
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade", order = 0)]
    public class UpgradeScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_UpgradeName = String.Empty;
        [SerializeField] private string m_Description = String.Empty;
        [SerializeField] private int m_MaxLevel = 100;
        [SerializeField] private string m_TargetValueName = String.Empty;
        
        [Header("Cost Curve")]
        [SerializeField] private AnimationCurve m_CostCurve = AnimationCurve.Linear(0,0,1,1); 
        [SerializeField] private Vector2 m_MinMaxCost = Vector2.zero;

        public string TargetValueName => m_TargetValueName;
        public string UpgradeName => m_UpgradeName;

        public float GetCost(int level)
        {
            return Mathf.Lerp(m_MinMaxCost.x,m_MinMaxCost.y,m_CostCurve.Evaluate((float)level/m_MaxLevel));
        }

        public virtual Upgrade GetUpgrade()
        {
            return null;
        }
    }
}