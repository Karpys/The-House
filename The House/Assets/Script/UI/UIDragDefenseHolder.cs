namespace Script.UI
{
    using System;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;


    public interface IUIDraggableToScene
    {
        void Select();
        void Release();
        void OnUI();
        void OnScene();
        void OnMove();
    }

    public enum DragState
    {
        None,
        Selection,
        Move,
    }

    public enum DraggableToSceneState
    {
        UI,
        Scene,
    }

    public class UIDragDefenseHolder : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Body = null;
        [SerializeField] private RectTransform m_ArrowIcon = null;
        private bool m_InDisplay = false;
        
        public void DisplayButton()
        {
            if (!m_InDisplay)
            {
                Display();
            }
            else
            {
                Hide();
            }
        }

        private void Display()
        {
            m_Body.DoAnchorPosition(new Vector2(0, 0), 0.15f);
            m_ArrowIcon.DoRotate(new Vector3(0, 0, 180), 0.15f).SetMode(TweenMode.ADDITIVE);
            m_InDisplay = true;
        }
        
        private void Hide()
        {
            m_Body.DoAnchorPosition(new Vector2(150, 0), 0.15f);
            m_ArrowIcon.DoRotate(new Vector3(0, 0, 180), 0.15f).SetMode(TweenMode.ADDITIVE);
            m_InDisplay = false;
        }
    }
}