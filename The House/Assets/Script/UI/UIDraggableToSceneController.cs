namespace Script.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem;

    public class UIDraggableToSceneController : MonoBehaviour
    {
        private DragState m_DragState = DragState.None; 

        private DraggableToSceneState m_DraggableToSceneState = DraggableToSceneState.UI;

        private IUIDraggableToScene m_CurrentDraggable = null;
        private void Update()
        {
            if(m_CurrentDraggable == null)
                return;
            
            switch (m_DragState)
            {
                case DragState.Move:
                    m_CurrentDraggable.OnMove();

                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        //Still on UI
                        if (m_DraggableToSceneState == DraggableToSceneState.Scene)
                        {
                            m_DraggableToSceneState = DraggableToSceneState.UI;
                            m_CurrentDraggable.OnUI();
                        }
                    }
                    else
                    {
                        if (m_DraggableToSceneState == DraggableToSceneState.UI)
                        {
                            m_CurrentDraggable.OnScene();
                            m_DraggableToSceneState = DraggableToSceneState.Scene;
                        }
                    }
                    break;
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
                Release();
        }

        private void Release()
        {
            m_CurrentDraggable.Release();
            m_CurrentDraggable = null;
        }

        public void Select(IUIDraggableToScene draggableToScene)
        {
            m_DragState = DragState.Move;
            m_CurrentDraggable = draggableToScene;
        }
    }
}