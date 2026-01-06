namespace Script
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class BaseProjectileLogic : ProjectileLogic
    {
        private Transform m_Target = null;
        private BaseProjectile m_BaseProjectile = null;

        public BaseProjectileLogic(Transform target,BaseProjectile baseProjectile)
        {
            m_Target = target;
            m_BaseProjectile = baseProjectile;
        }
        
        public override void Behave()
        {
            if (!m_Target)
            {
                MoveForward();   
                return;
            }
            
            MoveTowards(m_Target.position);
            m_BaseProjectile.RotateTowards(m_Target.position.Vec2());
        }

        public void MoveTowards(Vector2 position)
        {
            Vector2 targetPosition = m_BaseProjectile.transform.position.Vec2() + (position - m_BaseProjectile.transform.position.Vec2()).normalized * (m_BaseProjectile.ProjectileSpeed * Time.fixedDeltaTime);
            m_BaseProjectile.transform.position = targetPosition;
        }

        public void MoveForward()
        {
            Vector2 targetPosition = m_BaseProjectile.transform.position.Vec2() + m_BaseProjectile.transform.right.Vec2() * (m_BaseProjectile.ProjectileSpeed * Time.fixedDeltaTime);
            m_BaseProjectile.transform.position = targetPosition;
        }
    }
}