namespace Script.Data
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewTowerInfo", menuName = "Tower Info", order = 0)]
    public class DefenseInfoScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_DefenseName = String.Empty;
        [SerializeField] private Sprite m_TowerIcon = null;

        public string DefenseName => m_DefenseName;
        public Sprite Sprite => m_TowerIcon;
    }
}