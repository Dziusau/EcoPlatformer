using System.Collections.Generic;
using UnityEngine;

public class PoisonedCeiling : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_drops;
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float seed = 0.5f;

    private Collider2D m_collider;
    private float timer = 0.0f;

    private void Awake()
    {
        m_collider = GetComponent<CompositeCollider2D>();
    }

    private void FixedUpdate()
    {
        if (m_drops != null)
        {
            timer += Time.fixedDeltaTime;

            if (timer > delay) 
            {
                timer = 0.0f;
                delay = Random.Range(delay - seed, delay + seed);
                int index = 0;
                if (m_drops.Count != 1)
                    index = Random.Range(0, m_drops.Count - 1);
                
                Vector3 position = new Vector3(Random.Range(m_collider.bounds.center.x - m_collider.bounds.size.x, m_collider.bounds.center.x + m_collider.bounds.size.x), 
                    (m_collider.bounds.center.y - m_collider.bounds.size.y), 
                    m_collider.bounds.center.z);

                Instantiate(m_drops[index], position, Quaternion.identity);
            }
        }
    }
}
