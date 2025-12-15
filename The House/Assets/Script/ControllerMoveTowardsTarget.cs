namespace Script
{
    using Behaviour;

    public class ControllerMoveTowardsTarget : IBehave
    {
        private MoveTowardsTarget m_MoveTowardsTarget = null;
        public ControllerMoveTowardsTarget(ITarget closestTarget,IController projectileController)
        {
            m_MoveTowardsTarget = new MoveTowardsTarget(closestTarget.Transform, projectileController);
        }

        public void Behave()
        {
            m_MoveTowardsTarget.Behave();
        }
    }
}