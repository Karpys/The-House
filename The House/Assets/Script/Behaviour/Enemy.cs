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
        [SerializeField] private float m_AttackRange = 0.5f;
        
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

        public void MoveTowards(Vector2 targetPosition)
        {
            Vector2 currentPosition = transform.position.Vec2();
            float distance = Vector2.Distance(currentPosition, targetPosition);

            if (distance <= m_AttackRange)
                return;

            float maxStep = m_Speed * Time.fixedDeltaTime;
            float distanceToMove = distance - m_AttackRange;
            float step = Mathf.Min(maxStep, distanceToMove);

            Vector2 direction = (targetPosition - currentPosition).normalized;
            Vector2 nextMove = currentPosition + direction * step;

            m_Rigidbody.MovePosition(nextMove);
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