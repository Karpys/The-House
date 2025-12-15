namespace Script.Behaviour
{
    using UnityEngine;

    public class MoveTowardsTarget : IBehave
    {
        private Transform m_Target = null;
        private IController m_Controller = null;
        
        public MoveTowardsTarget(Transform target, IController controller)
        {
            m_Target = target;
            m_Controller = controller;
        }
        
        public void Behave()
        {
            if(m_Target == null)
                return;
            m_Controller.MoveTowards(m_Target.position);
        }
    }
}