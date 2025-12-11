using KarpysDev.KarpysUtils;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private Transform m_CenterPosition = null;
    [SerializeField] private Transform m_EnemyPrefab = null;

    [Header("Spawn")]
    [SerializeField] private float m_SpawnDelay = 2f;
    [SerializeField] private float m_SpawnCount = 10;
    [SerializeField] private float m_SpawnRange = 5;

    private Loop m_SpawnClock = null;

    private void Awake()
    {
        m_SpawnClock = new Loop(m_SpawnDelay, SpawnEnemy);
    }

    void Update()
    {
        m_SpawnClock?.Update();
    }
    
    private void SpawnEnemy()
    {
        Vector2 spawnPosition = m_CenterPosition.position.Vec2() + Random.insideUnitCircle.normalized * m_SpawnRange;
        Transform en = Instantiate(m_EnemyPrefab, spawnPosition, Quaternion.identity);
        m_SpawnCount -= 1;
        
        if(m_SpawnCount <= 0)
            m_SpawnClock.Stop();
    }
}
