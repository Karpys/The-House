namespace Script.Behaviour
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class Enemy : MonoBehaviour,ITarget,IDamageable
    {
        [SerializeField] private Rigidbody2D m_Rigidbody = null;
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private float m_Life = 10f;
        [SerializeField] private float m_AttackRange = 0.5f;
        [SerializeField] private float m_ContactDamage = 1;

        [SerializeField] private AnimationCurve m_AttackAnimationCurve = null;
        [SerializeField] private float m_AttackDuration = 0.5f;
        
        public Transform Transform => transform;
        public Vector2 Position => transform.position.Vec2();
        public float ContactDamage => m_ContactDamage;
        public AnimationCurve AttackAnimationCurve => m_AttackAnimationCurve;
        public float AttackDuration => m_AttackDuration;

        private EnemyAttack m_DefaultAttack = null;
        private Transform m_Target = null;
        private void Awake()
        {
            m_DefaultAttack = new EnemyAttack(this);
            m_Target = GameManager.Instance.House;
        }

        private void FixedUpdate()
        {
            Behave();
        }

        private void Behave()
        {
            if (m_Target && !m_DefaultAttack.InAttack)
            {
                MoveTowardsTarget(m_Target);
            }
        }

        public void Move(Vector2 position)
        {
            m_Rigidbody.MovePosition(position);
        }

        public void MoveTowardsTarget(Transform target)
        {
            Vector2 targetPosition = target.position.Vec2();
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
            if(m_DefaultAttack.InAttack)
                return;
            
            m_DefaultAttack.Attack(m_Target);
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