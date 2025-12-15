namespace Script
{
    using Behaviour;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class BaseProjectile : MonoBehaviour,IController
    {
        private float m_ProjectileSpeed = 0;
        private IBehave m_ProjectileBehavior = null;
        private float m_Damage = 0;
        
        public void InitializeBaseProjectile(float damage,float projectileSpeed,IBehave projectileBehavior)
        {
            m_Damage = damage;
            m_ProjectileSpeed = projectileSpeed;
            m_ProjectileBehavior = projectileBehavior;
        }

        private void FixedUpdate()
        {
            m_ProjectileBehavior?.Behave();
        }

        public void Move(Vector2 position)
        {
            transform.position = position;
        }

        public void MoveTowards(Vector2 position)
        {
            Vector2 targetPosition = transform.position.Vec2() + (position - transform.position.Vec2()).normalized * (m_ProjectileSpeed * Time.fixedDeltaTime);
            transform.position = targetPosition;
        }

        public void MoveForward()
        {
            Vector2 targetPosition = transform.position.Vec2() + transform.right.Vec2() * (m_ProjectileSpeed * Time.fixedDeltaTime);
            transform.position = targetPosition;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(m_Damage);
                Destroy(gameObject);
            }
        }
    }

    public interface IDamageable
    {
        public void TakeDamage(float damage);
    }
}