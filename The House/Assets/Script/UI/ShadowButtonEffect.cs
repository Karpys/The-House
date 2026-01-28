namespace Script.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ShadowButtonEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Body = null;
        [SerializeField] private Shadow m_Shadow = null;
        [SerializeField] private float m_Duration = 0.15f;

        public void Effect()
        {
            float force = m_Shadow.effectDistance.y;
            m_Body.anchoredPosition = new Vector2(m_Body.anchoredPosition.x, m_Body.anchoredPosition.y + force);
            m_Shadow.enabled = false;
            Invoke(nameof(ResetEffect), m_Duration);
        }

        private void ResetEffect()
        {
            m_Shadow.enabled = true;
            float force = m_Shadow.effectDistance.y;
            m_Body.anchoredPosition = new Vector2(m_Body.anchoredPosition.x, m_Body.anchoredPosition.y - force);
        }
    }
}