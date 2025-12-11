namespace Script
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        [SerializeField] private Transform m_House = null;

        public Transform House => m_House;
    }
}