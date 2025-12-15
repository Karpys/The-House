namespace Script
{
    using System.Collections.Generic;
    using Behaviour;
    using UnityEngine;

    public class TargetSelector
    {
        private List<ITarget> m_Targetables = new List<ITarget>();

        public void Record(ITarget target)
        {
            m_Targetables.Add(target);
        }
        
        public void Remove(ITarget target)
        {
            m_Targetables.Remove(target);
        }

        public ITarget SelectClosest(Vector2 position)
        {
            if (m_Targetables.Count == 0)
                return null;

            ITarget closest = null;
            float closestDistance = 1000000;

            for (int i = 0; i < m_Targetables.Count; i++)
            {
                if (m_Targetables[i] == null)
                {
                    m_Targetables.RemoveAt(i);
                    i--;
                    continue;
                }
                
                float distance = Vector2.Distance(m_Targetables[i].Position, position);

                if (distance <= closestDistance)
                {
                    closest = m_Targetables[i];
                    closestDistance = distance;
                }
            }

            return closest;
        }
    }
}