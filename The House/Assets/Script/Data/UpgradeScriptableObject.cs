namespace Script.Data
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade", order = 0)]
    public class UpgradeScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_Name = String.Empty;
        [SerializeField] private string m_Description = String.Empty;
        [SerializeField] private int m_MaxLevel = 100;

        public float GetCost(int level)
        {
            return 10;
        }
    }
}