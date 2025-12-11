namespace Script.Behaviour
{
    using UnityEngine;

    public class MoveTowardsTowerBehaviour : IBehave
    {
        private Transform m_Tower = null;
        private IController m_Controller = null;
        
        public MoveTowardsTowerBehaviour(Transform tower, IController controller)
        {
            m_Tower = tower;
            m_Controller = controller;
        }
        
        public void Behave()
        {
            m_Controller.MoveTowards(m_Tower.position);
        }
    }
}