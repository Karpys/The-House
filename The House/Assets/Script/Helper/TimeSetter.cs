namespace Script.Helper
{
    using System;
    using UnityEngine;

    public class TimeSetter : MonoBehaviour
    {
        [SerializeField] private float m_TimeScale = 1;


        public void Update()
        {
            Time.timeScale = m_TimeScale;
        }
    }
}