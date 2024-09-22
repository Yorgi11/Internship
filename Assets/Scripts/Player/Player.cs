using System.Collections;
using UnityEngine;
using MGUtilities;
public class Player : MonoBehaviour
{

    [SerializeField] private Gun m_currentGun;

    private bool m_sprintInput;
    private bool m_jumpInput;
    private float m_verticalInput;
    private float m_horizontalInput;

    private Health m_health;
    private Move m_move;
    private CamCTRL m_camCTRL;
    void Start()
    {
        m_move = GetComponent<Move>();
        m_camCTRL = GetComponentInChildren<CamCTRL>();
        m_health = GetComponent<Health>();
    }
    void Update()
    {
        if (GameManager.Instance().GAMEOVER) return;

        m_camCTRL.UpdateCamera(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        m_sprintInput = Input.GetKey(KeyCode.LeftShift);
        m_jumpInput = Input.GetKey(KeyCode.Space);
        m_verticalInput = Input.GetAxisRaw("Vertical");
        m_horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Mouse0) && m_currentGun) m_currentGun.Shoot();
    }
    private void FixedUpdate()
    {
        m_move.MovePhysics(m_verticalInput, m_horizontalInput, m_sprintInput, m_jumpInput);
    }
    public void TakeDamage(float d)
    {
        m_health.TakeDamage(d);
    }
}