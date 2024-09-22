using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_attackDistance;
    [SerializeField] private float m_chaseDistance;
    [SerializeField] private float m_shootDistance;
    [SerializeField] private float m_meleeDamage;
    [SerializeField] private float m_meleeAttackTime;
    [SerializeField] private Gun m_gun;

    private bool m_canAttack = true;

    private Vector3 m_dirToPlayer;

    private Transform m_playerTransform;
    private Player m_player;
    private NavMeshAgent m_agent;
    void Start()
    {
        m_player = FindObjectOfType<Player>();
        m_playerTransform = m_player.transform;
        m_agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        m_dirToPlayer = (m_playerTransform.position - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, m_dirToPlayer, 8f);
        float dist = Vector3.Distance(transform.position, m_playerTransform.position);
        if (dist <= m_attackDistance && m_canAttack) StartCoroutine(Attack());
        else if (dist <= m_chaseDistance) m_agent.SetDestination(m_playerTransform.position);
        else if (dist <= m_shootDistance)
        {
            transform.forward = m_dirToPlayer;
            m_gun.Shoot();
        }
    }
    private IEnumerator Attack()
    {
        m_canAttack = false;
        if (Mathf.Acos(Vector3.Dot(transform.forward, m_dirToPlayer)) < 20f) m_player.TakeDamage(m_meleeDamage);
        yield return new WaitForSeconds(m_meleeAttackTime);
        m_canAttack = true;
    }
}