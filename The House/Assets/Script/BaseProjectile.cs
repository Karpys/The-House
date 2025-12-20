namespace Script
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class BaseProjectile : MonoBehaviour
    {
        private float m_ProjectileSpeed = 0;
        private float m_Damage = 0;
        private Transform m_Target = null;
        public void InitializeBaseProjectile(float damage,float projectileSpeed)
        {
            m_Damage = damage;
            m_ProjectileSpeed = projectileSpeed;
        }

        private void FixedUpdate()
        {
            Behave();
        }

        private void Behave()
        {
            if (!m_Target)
            {
                Debug.Log("Move Forward");
                MoveForward();   
                return;
            }
            
            MoveTowards(m_Target.position);
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

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }
    }

    public interface IDamageable
    {
        public void TakeDamage(float damage);
    }
}