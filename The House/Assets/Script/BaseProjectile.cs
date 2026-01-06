namespace Script
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;


    public class BaseProjectile : MonoBehaviour
    {
        private float m_ProjectileSpeed = 0;
        private float m_Damage = 0;

        private ProjectileLogic m_Logic = null;
        public float ProjectileSpeed => m_ProjectileSpeed;

        public void InitializeBaseProjectile(ProjectileLogic projectileLogic, float damage, float projectileSpeed)
        {
            m_Logic = projectileLogic;
            m_Damage = damage;
            m_ProjectileSpeed = projectileSpeed;
        }

        private void FixedUpdate()
        {
            m_Logic.Behave();
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

        private void SetAngleRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void RotateTowards(Vector2 position)
        {
            Vector2 direction = position - transform.position.Vec2();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            SetAngleRotation(angle);
        }
    }

    public interface IDamageable
    {
        public void TakeDamage(float damage);
    }
}