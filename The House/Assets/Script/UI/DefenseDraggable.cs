namespace Script.UI
{
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.UI;
    using UnityEngine;

    public class DefenseDraggable : EventButtonPointer,IUIDraggableToScene
    {
        [SerializeField] private UIDraggableToSceneController m_Controller = null;
        [SerializeField] private ArcherBehaviour m_DefenseToCreate = null;
        [SerializeField] private Transform m_ShadowPrefab;
        [SerializeField] private bool m_IsLock = false;

        private Transform m_Shadow = null;
        private void Awake()
        {
            m_Shadow = Instantiate(m_ShadowPrefab);
            m_Shadow.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if(m_Shadow && m_Shadow.gameObject)
                Destroy(m_Shadow.gameObject);
        }

        public void Select()
        {
            if(m_IsLock)
                return;
            m_Controller.Select(this);
        }

        public void Release()
        {
            if (m_Shadow.gameObject.activeSelf)
                CreateDefense();
        }

        public void OnUI()
        {
            m_Shadow.gameObject.SetActive(false);
        }

        public void OnScene()
        {
            m_Shadow.gameObject.SetActive(true);
        }

        public void OnMove()
        {
            m_Shadow.position = PositionUtils.MouseToWorld();
        }
        
        private void CreateDefense()
        {
            m_IsLock = true;
            m_Shadow.gameObject.SetActive(false);
            Instantiate(m_DefenseToCreate, PositionUtils.MouseToWorld(), Quaternion.identity);
        }
    }
}