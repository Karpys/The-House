namespace Script.Behaviour
{
    using System.Security.Cryptography;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class Enemy : MonoBehaviour,IController,ITarget,IDamageable
    {
        [SerializeField] private Rigidbody2D m_Rigidbody = null;
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private float m_Life = 10f;
        
        private IBehave m_Behaviour = null;

        public Transform Transform => transform;
        public Vector2 Position => transform.position.Vec2();

        private void Awake()
        {
            m_Behaviour = new MoveTowardsTarget(GameManager.Instance.House, this);
        }

        private void FixedUpdate()
        {
            m_Behaviour.Behave();
        }
        
        public void Move(Vector2 position)
        {
            m_Rigidbody.MovePosition(position);
        }

        public void MoveTowards(Vector2 position)
        {
            Vector2 targetPosition = transform.position.Vec2() + (position - transform.position.Vec2()).normalized * (m_Speed * Time.fixedDeltaTime); 
            m_Rigidbody.MovePosition(targetPosition);
        }

        public void MoveForward()
        {
            return;
        }

        public void TakeDamage(float damage)
        {
            m_Life -= damage;

            if (m_Life <= 0)
                TriggerDeath();
        }

        private void TriggerDeath()
        {
            Destroy(gameObject);
        }
    }
}