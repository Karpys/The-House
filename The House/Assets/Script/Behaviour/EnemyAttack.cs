namespace Script.Behaviour
{
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class EnemyAttack
    {
        private bool m_InAttack = false;
        private Enemy m_Enemy = null;
        public bool InAttack => m_InAttack;
        public EnemyAttack(Enemy enemy)
        {
            m_Enemy = enemy;
        }

        public void Attack(Transform target)
        {
            m_InAttack = true;
            IDamageable damageable = target.GetComponent<IDamageable>();
            m_Enemy.transform.DoMove(target.position, m_Enemy.AttackDuration).SetCurve(m_Enemy.AttackAnimationCurve).OnComplete(ResetAttackDelay);
            FixedMethodDelayer.Instance.AddMethod(() => DealDamage(damageable),m_Enemy.AttackDuration/2);
        }

        private void ResetAttackDelay()
        {
            FixedMethodDelayer.Instance.AddMethod(ResetAttack, 0.5f);
        }

        private void ResetAttack()
        {
            m_InAttack = false;
        }

        private void DealDamage(IDamageable damageable)
        {
            damageable?.TakeDamage(m_Enemy.ContactDamage);
        }
    }
}