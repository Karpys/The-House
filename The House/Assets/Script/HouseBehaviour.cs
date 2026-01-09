namespace Script
{
    using UnityEngine;

    public class HouseBehaviour : BaseDefense,IDamageable
    {
        [SerializeField] private float m_Life = 100;
        public void TakeDamage(float damage)
        {
            Debug.Log("Take damage : " + damage);
            m_Life -= damage;
        }
    }
}