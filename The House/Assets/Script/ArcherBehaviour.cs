namespace Script
{
    using Behaviour;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class ArcherBehaviour : BaseDefense
    {
        [SerializeField] private float m_Range = 1;
        [SerializeField] private CircleCollider2D m_RangeTrigger = null;
        [SerializeField] private RangeVisualIndicator m_RangeVisualIndicator = null;
        
        [Header("Damage")]
        [SerializeField] private float m_ProjectileDamage = 10;
        [SerializeField] private float m_ProjectileSpeed = 10;
        [SerializeField] private float m_AttackSpeed = 1;
        [SerializeField] private BaseProjectile m_ProjectilePrefab = null;
        
        private TargetSelector m_TargetSelector = new TargetSelector();
        private Clock m_ShootClock = null;

        private void Awake()
        {
            m_ShootClock = new Clock(m_AttackSpeed, TryShoot);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                m_TargetSelector.Record(target);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                m_TargetSelector.Remove(target);
            }
        }

        private void FixedUpdate()
        {
            m_ShootClock?.FixedUpdateClock();
        }
        
        private void OnDrawGizmos()
        {
            UpdateRange();
        }

        private void UpdateRange()
        {
            m_RangeTrigger.radius = m_Range;
            m_RangeVisualIndicator.UpdateRange(m_Range);
            m_RangeVisualIndicator.transform.localScale = Vector3.one * m_Range;
        }
        
        private void TryShoot()
        {
            ITarget closestTarget = m_TargetSelector.SelectClosest(transform.position.Vec2());

            if (closestTarget != null)
            {
                ShootTo(closestTarget);
                m_ShootClock.Restart(m_AttackSpeed);
            }
            else
            {
                m_ShootClock.Restart(0f);
            }
        }

        private void ShootTo(ITarget closestTarget)
        {
            BaseProjectile proj = Instantiate(m_ProjectilePrefab, transform.position, Quaternion.identity);
            proj.RotateTowards(closestTarget.Transform.position.Vec2());
            proj.InitializeBaseProjectile(new BaseProjectileLogic(closestTarget.Transform,proj), m_ProjectileDamage,m_ProjectileSpeed);
        }
    }
}