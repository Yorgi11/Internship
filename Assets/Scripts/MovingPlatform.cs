using UnityEngine;
using MGUtilities;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float m_timeToMove;
    [SerializeField] private float m_distToMove;
    private Vector3 m_start;
    private Vector3 m_end;
    private bool m_move = true;
    private void Start()
    {
        m_start = transform.position;
        m_end = transform.position + (m_distToMove * transform.forward);
    }
    void Update()
    {
        if (m_move) StartCoroutine(Coroutines.PingPongVector3OverTime(m_start, m_end, m_timeToMove, value => m_move = value, value => transform.position = value));
    }
}