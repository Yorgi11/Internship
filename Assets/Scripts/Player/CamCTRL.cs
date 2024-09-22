using UnityEngine;
public class CamCTRL : MonoBehaviour
{
    [Header("Camera Control")]
    [SerializeField] private float m_mouseSensitivity = 200f;

    private float m_xRot = 0f, m_yRot = 0f;

    private Transform m_transform;
    private void Awake()
    {
        m_transform = transform;
        Initialize();
    }
    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UpdateCamera(float mouseX, float mouseY)
    {
        m_xRot -= (mouseY * m_mouseSensitivity * Time.fixedDeltaTime);
        m_yRot += (mouseX * m_mouseSensitivity * Time.fixedDeltaTime);
        if (m_xRot > 70f) m_xRot = 70f;
        if (m_xRot < -70f) m_xRot = -70f;

        m_transform.parent.rotation = Quaternion.Euler(0f, m_yRot, 0f);
        m_transform.rotation = Quaternion.Euler(m_xRot, m_yRot, 0f);
    }
}