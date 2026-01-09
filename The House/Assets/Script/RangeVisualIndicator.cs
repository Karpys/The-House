namespace Script
{
    using UnityEngine;

    public class RangeVisualIndicator : MonoBehaviour
    {
        [SerializeField] private float m_RangeBaseSize = 1;
        [SerializeField] private Renderer m_Renderer = null;

        public void UpdateRange(float range)
        {
            if (Application.isPlaying)
            {
                m_Renderer.material.SetFloat("_Size",m_RangeBaseSize / range);
            }
            else
            {
                m_Renderer.sharedMaterial.SetFloat("_Size",m_RangeBaseSize / range);
            }
        }
    }
}