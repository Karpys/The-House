namespace Script.Behaviour
{
    using System;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class Enemy : MonoBehaviour,ITarget,IDamageable
    {
        [SerializeField] private Rigidbody2D m_Rigidbody = null;
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private float m_Life = 10f;
        [SerializeField] private float m_AttackRange = 0.5f;
        
        public Transform Transform => transform;
        public Vector2 Position => transform.position.Vec2();

        private Transform m_Target = null;
        private bool m_InAttack = false;
        private void Awake()
        {
            m_Target = GameManager.Instance.House;
        }

        private void FixedUpdate()
        {
            Behave();
        }

        private void Behave()
        {
            if (m_Target && !m_InAttack)
            {
                MoveTowards(m_Target.position.Vec2());
            }
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
            {
                TriggerAttack();
                return;
            }

            float maxStep = m_Speed * Time.fixedDeltaTime;
            float distanceToMove = distance - m_AttackRange;
            float step = Mathf.Min(maxStep, distanceToMove);

            Vector2 direction = (targetPosition - currentPosition).normalized;
            Vector2 nextMove = currentPosition + direction * step;

            m_Rigidbody.MovePosition(nextMove);
        }

        private void TriggerAttack()
        {
            m_InAttack = true;
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