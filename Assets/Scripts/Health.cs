using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private bool m_isPlayer;
    [SerializeField] private Text m_text;
    [SerializeField] private TextMesh m_textMesh;
    [SerializeField] private float m_maxHp;
    private float m_currentHp;
    void Start()
    {
        m_currentHp = m_maxHp;
    }
    private void Update()
    {
        if (m_text) m_text.text = "HP: " + m_currentHp.ToString();
        if (m_textMesh) m_textMesh.text = "HP: " + m_currentHp.ToString();
    }
    public void TakeDamage(float d)
    {
        m_currentHp -= d;
        if (m_currentHp <= 0f) Die();
    }
    private void Die()
    {
        m_currentHp = 0f;
        if (m_isPlayer) GameManager.Instance().ShowGameOverScreen(false);
        else GameManager.Instance().ShowGameOverScreen(true);
    }
}