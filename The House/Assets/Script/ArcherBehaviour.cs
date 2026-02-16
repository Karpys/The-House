namespace Script
{
    using System.Collections.Generic;
    using Behaviour;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class CriticalModule
    {
        private float m_CritChance = 0f;
        private float m_CriticalMultiplier = 0f;

        public float CriticalMultiplier => m_CriticalMultiplier;

        public CriticalModule(float critChance, float criticalMultiplier)
        {
            m_CritChance = critChance;
            m_CriticalMultiplier = criticalMultiplier;
        }

        public void SetCriticalChance(float critChance)
        {
            m_CritChance = critChance;
        }

        public void SetCriticalMultiplier(float critMultiplier)
        {
            m_CriticalMultiplier = critMultiplier;
        }
        
        private bool IsCritical()
        {
            return FloatUtils.PercentChance(m_CritChance);
        }
    }
    
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
        [SerializeField] private float m_BaseCriticalChance = 0;
        [SerializeField] private float m_BaseCriticalMultiplier = 2;

        [Header("MultiShoot")]
        [SerializeField] private float m_MultiShootPercentChance = 0f;
        [SerializeField] private int m_MultiShootCount = 2;
        
        [Header("Computed Stats / Useless to change in prefab mode")]
        [SerializeField] private float m_ProjectileDamage = 0;
        [SerializeField] private float m_ProjectileSpeed = 0;
        [SerializeField] private float m_AttackSpeed = 20;
        [SerializeField] private float m_Range = 0;
        
        private TargetSelector m_TargetSelector = new TargetSelector();
        private Clock m_ShootClock = null;

        protected override void Awake()
        {
            base.Awake();
            m_ShootClock = new Clock(1/m_BaseAttackSpeed, TryShoot);
            InitializeDefenseValues();
            ApplyUpgrade();
        }

        protected override void ApplyUpgrade()
        {
            ApplyBaseValue();

            //Need to correctly summs linear values
            // Exemple :
            // Two +10% range => 100 * 1.1 => 110 * 1.1 => 121 != 120
            // Need Design Balance and Choice
            foreach (Upgrade upgrade in m_Upgrades)
            {
                switch (upgrade.UpgradeType)
                {
                    case UpgradeType.PercentAttackSpeed:
                        float attackRatio = 1 + upgrade.Value.ToFloat() / 100;
                        m_AttackSpeed *= attackRatio;
                        break;
                    case UpgradeType.PercentDamage:
                        float damageRatio = 1 + upgrade.Value.ToFloat() / 100;
                        m_ProjectileDamage *= damageRatio;
                        break;
                    case UpgradeType.PercentRange:
                        float percentRange = 1 + upgrade.Value.ToFloat() / 100;
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
            m_DefenseValues.Add("Attack Speed",() => m_AttackSpeed.ToString());
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
            m_TargetSelector.SelectMultiple(m_MultiShootCount);
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
            if (IsMultiShot())
            {
                MultiShot();
            }
            else
            {
                SingleShot();
            }
        }

        private bool IsMultiShot()
        {
            return FloatUtils.PercentChance(m_MultiShootPercentChance);
        }
        
        private void MultiShot()
        {
            List<ITarget> targets = m_TargetSelector.SelectMultipleClosest(transform.position.Vec2(), m_MultiShootCount);

            if (targets == null)
            {
                m_ShootClock.Restart(0f);
                return;
            }
            
            foreach (ITarget target in targets)
            {
                if(target != null)
                    ShootTo(target);
            }

            if (targets.Count > 0)
            {
                m_ShootClock.Restart(1/m_AttackSpeed);
            }
            else
            {
                m_ShootClock.Restart(0f);
            }
        }

        private void SingleShot()
        {
            ITarget closestTarget = m_TargetSelector.SelectClosest(transform.position.Vec2());

            if (closestTarget != null)
            {
                ShootTo(closestTarget);
                m_ShootClock.Restart(1/m_AttackSpeed);
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