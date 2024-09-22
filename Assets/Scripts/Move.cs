using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour
{
    [Header("Max Speed")]
    [SerializeField] private float m_walkSpeed;
    [SerializeField] private float m_runSpeed;
    [SerializeField] private float m_strafeFactor;
    [SerializeField] private float m_jumpSpeed;
    [Header("Acceleration")]
    [SerializeField] private float m_walkAcceleration;
    [SerializeField] private float m_runAcceleration;
    [Header("Acceleration")]
    [SerializeField] private float m_inAirCtrlFactor;
    [Header("Ground Check")]
    [SerializeField] private int m_numberOfGroundChecks;
    [SerializeField] private float m_groundDist;
    [SerializeField] private LayerMask m_groundLayers;
    [Header("Physics Material")]
    [SerializeField] private PhysicMaterial m_pm;

    public bool IsGrounded { get; private set; }

    private Vector3 m_groundNormal;

    private float m_currentSpeed;
    private float m_currentAcceleration;

    private Vector3 m_flatVelocity = Vector3.zero;
    private Vector3 m_currentVelocity = Vector3.zero;

    private Transform m_transform;

    public Rigidbody Rb { get; private set; }
    private void Awake()
    {
        m_transform = transform;
        Rb = GetComponent<Rigidbody>();
    }
    void OnGUI()
    {
        if (GetComponent<Player>() != null)
        {
            GUIStyle debugStyle = new(GUI.skin.label)
            {
                fontSize = 20,
                padding = new RectOffset(15, 15, 10, 10)
            };
            string debugMessage = $"flatVelocity: {m_flatVelocity.magnitude:F1}, fullVelocity: {m_currentVelocity.magnitude:F1}";
            GUILayout.Label(debugMessage, debugStyle);
        }
    }
    public void MovePhysics(float vert, float horz, bool isSprinting, bool jump)
    {
        IsGrounded = CheckGround();
        m_currentSpeed = isSprinting ? m_runSpeed : m_walkSpeed;
        m_currentAcceleration = isSprinting ? m_runAcceleration : m_walkAcceleration;

        m_currentVelocity = Rb.velocity;
        Vector3 targetVel = m_currentSpeed * Vector3.ProjectOnPlane(vert * m_transform.forward + m_strafeFactor * horz * m_transform.right, m_groundNormal);
        if (targetVel.magnitude > m_currentSpeed) targetVel = m_currentSpeed * targetVel.normalized;
        m_flatVelocity = m_currentVelocity - Vector3.Project(m_currentVelocity, m_groundNormal);

        if (jump && IsGrounded)
        {
            Rb.velocity = new Vector3(m_flatVelocity.x, 0f, m_flatVelocity.z);
            Rb.AddForce(Rb.mass * m_jumpSpeed * m_transform.up, ForceMode.Impulse);
        }

        if (IsGrounded) Rb.AddForce(Rb.mass * m_currentAcceleration * (targetVel - m_flatVelocity) - (m_pm.dynamicFriction * Rb.mass * Physics.gravity), ForceMode.Force);
        else
        {
            if (Physics.Raycast(m_transform.position, targetVel.normalized, out RaycastHit hit, 0.75f, m_groundLayers, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider != null)
                {
                    Rb.AddForce(Rb.mass * m_currentAcceleration * (hit.normal - m_flatVelocity), ForceMode.Force);
                }
            }
            else Rb.AddForce(m_inAirCtrlFactor * Rb.mass * m_currentAcceleration * (targetVel - m_flatVelocity), ForceMode.Force);
        }
    }
    private bool CheckGround()
    {
        float angleStep = 360f / m_numberOfGroundChecks;
        for (int i = 0; i < m_numberOfGroundChecks; i++)
        {
            float angleInRadians = Mathf.Deg2Rad * angleStep * i;
            float x = transform.position.x + 0.5f * Mathf.Cos(angleInRadians);
            float z = transform.position.z + 0.5f * Mathf.Sin(angleInRadians);

            if (Physics.Raycast(new Vector3(x, transform.position.y + 1f, z), Vector3.down, out RaycastHit hit, m_groundDist + 1f, m_groundLayers, QueryTriggerInteraction.Ignore))
            {
                m_groundNormal = hit.normal;
                return true;
            }
        }
        return false;
    }
}