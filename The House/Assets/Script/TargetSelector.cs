namespace Script
{
    using System.Collections.Generic;
    using System.Linq;
    using Behaviour;
    using KarpysDev.KarpysUtils;
    using NUnit.Framework;
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

        private List<int> m_CachedResults = new List<int>();
        private List<float> m_CachedDistance = new List<float>();
        public List<ITarget> SelectMultipleClosest(Vector2 position, int count)
        {
            if (m_Targetables.Count == 0)
                return null;
            
            m_CachedResults.Clear();
            m_CachedDistance.Clear();
            
            float worseDistance = 0;
            int worseId = 0;

            for (int i = 0; i < m_Targetables.Count; i++)
            {
                if (m_Targetables[i] == null)
                {
                    m_Targetables.RemoveAt(i);
                    i--;
                    continue;
                }

                float distance = Vector2.Distance(m_Targetables[i].Position, position);
                m_CachedDistance.Add(distance);

                if (m_CachedResults.Count < count)
                {
                    m_CachedResults.Add(i);

                    if (distance > worseDistance)
                    {
                        worseDistance = distance;
                        worseId = i;
                    }
                }
                else
                {
                    //New result / replace worseDistance and worseId
                    if (distance < worseDistance)
                    {
                        m_CachedResults[worseId] = i;

                        int tempWorseId = 0;
                        float tempWorseDistance = m_CachedDistance[m_CachedResults[0]];
                        
                        for (int j = 1; j < m_CachedResults.Count; j++)
                        {
                            if (m_CachedDistance[m_CachedResults[j]] > tempWorseDistance)
                            {
                                tempWorseId = j;
                                tempWorseDistance = m_CachedDistance[tempWorseId];
                            }
                        }

                        worseDistance = tempWorseDistance;
                        worseId = tempWorseId;
                    }
                }
            }

            List<ITarget> result = new List<ITarget>();

            foreach (int cachedResult in m_CachedResults)
            {
                result.Add(m_Targetables[cachedResult]);
            }

            return result;
        }
    }
}