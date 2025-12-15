namespace Script
{
    using Behaviour;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class HouseBehaviour : MonoBehaviour
    {
        [SerializeField] private float m_Life = 100;
        
        [Header("Damage")]
        [SerializeField] private float m_ProjectileDamage = 10;
        [SerializeField] private float m_ProjectileSpeed = 10;
        [SerializeField] private float m_AttackSpeed = 1;
        [SerializeField] private BaseProjectile m_ProjectilePrefab = null;

        private TargetSelector m_TargetSelector = new TargetSelector();
        private Clock m_ShootClock = null;
        private Vector2 m_SelfPosition = Vector2.zero;

        private void Awake()
        {
            m_SelfPosition = transform.position.Vec2();
            m_ShootClock = new Clock(m_AttackSpeed, TryShoot);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                Debug.Log("Record");
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
        
        private void TryShoot()
        {
            Debug.Log("Try shoot");
            ITarget closestTarget = m_TargetSelector.SelectClosest(m_SelfPosition);

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
            proj.InitializeBaseProjectile(m_ProjectileDamage,m_ProjectileSpeed, new ControllerMoveTowardsTarget(closestTarget,proj));
        }
    }
}