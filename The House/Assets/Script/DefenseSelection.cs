namespace Script
{
    using KarpysDev.KarpysUtils.UI;
    using UnityEngine;

    public class DefenseSelection : EventButtonPointer
    {
        [SerializeField] private BaseDefense m_BaseDefense = null;

        public void OnSelect()
        {
            GameManager.Instance.DisplayPanelDefense(m_BaseDefense);
        }
    }
}