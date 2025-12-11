namespace Script.Behaviour
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class Enemy : MonoBehaviour,IController
    {
        [SerializeField] private Rigidbody2D m_Rigidbody = null;
        //Enemy stats / speed / life / Damage
        [SerializeField] private float m_Speed = 5f;
        private IBehave m_Behaviour = null;

        private void Awake()
        {
            m_Behaviour = new MoveTowardsTowerBehaviour(GameManager.Instance.House, this);
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
    }
}