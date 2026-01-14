namespace Script
{
    using System.Collections.Generic;
    using Behaviour;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class ArcherBehaviour : BaseDefense
    {
        [SerializeField] private float m_BaseRange = 1;
        [SerializeField] private CircleCollider2D m_RangeTrigger = null;
        [SerializeField] private RangeVisualIndicator m_RangeVisualIndicator = null;
        
        [Header("Base Stats")]
        [SerializeField] private float m_BaseProjectileDamage = 10;
        [SerializeField] private float m_BaseProjectileSpeed = 10;
        [SerializeField] private float m_BaseAttackSpeed = 1;
        [SerializeField] private BaseProjectile m_ProjectilePrefab = null;

        [Header("Stats")]
        [SerializeField] private float m_ProjectileDamage = 0;
        [SerializeField] private float m_ProjectileSpeed = 0;
        [SerializeField] private float m_AttackSpeed = 0;
        [SerializeField] private float m_Range = 0;
        
        private TargetSelector m_TargetSelector = new TargetSelector();
        private Clock m_ShootClock = null;

        protected override void Awake()
        {
            base.Awake();
            m_ShootClock = new Clock(m_BaseAttackSpeed, TryShoot);
            InitializeDefenseValues();
            ApplyUpgrade();
        }

        protected override void ApplyUpgrade()
        {
            ApplyBaseValue();
            foreach (KeyValuePair<UpgradeType,Upgrade> upgrade in m_Upgrades)
            {
                switch (upgrade.Key)
                {
                    case UpgradeType.PercentAttackSpeed:
                        float attackRatio = 1 + upgrade.Value.Value.ToFloat() / 100;
                        m_AttackSpeed *= attackRatio;
                        break;
                    case UpgradeType.PercentDamage:
                        float damageRatio = 1 + upgrade.Value.Value.ToFloat() / 100;
                        m_ProjectileDamage *= damageRatio;
                        break;
                    case UpgradeType.PercentRange:
                        float percentRange = 1 + upgrade.Value.Value.ToFloat() / 100;
                        m_Range *= percentRange;
                        break;
                }
            }
        }

        private void ApplyBaseValue()
        {
            m_ProjectileDamage = m_BaseProjectileDamage;
            m_AttackSpeed = m_BaseAttackSpeed;
            m_ProjectileSpeed = m_BaseProjectileSpeed;
            m_Range = m_BaseRange;
        }

        private void InitializeDefenseValues()
        {
            m_DefenseValues.Add("Range",() => m_Range.ToString());
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