namespace Script
{
    using Data;
    using UnityEngine;

    public abstract class BaseDefense : MonoBehaviour
    {
        [SerializeField] private DefenseInfoScriptableObject m_DefenseInfo = null;

        public DefenseInfoScriptableObject DefenseInfo => m_DefenseInfo;
    }
}