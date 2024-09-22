using UnityEngine;
using MGUtilities;
public class Gun : MonoBehaviour
{
    [SerializeField] private bool m_isPlayer;
    [SerializeField] private float m_fireRate;
    [SerializeField] private float m_bulletSpeed;
    [SerializeField] private float m_bulletDamage;
    [SerializeField] private Transform m_bulletSpawn;
    private bool m_canShoot = true;
    private BulletPool m_bulletPool;
    private void Start()
    {
        m_bulletPool = BulletPool.Instance();
    }
    public void Shoot()
    {
        if (m_canShoot)
        {
            Bullet b = m_bulletPool.GetBullet(m_bulletSpawn.position, m_bulletSpawn.rotation);
            b.Rb.velocity = m_bulletSpeed * m_bulletSpawn.forward;
            b.Damage = m_bulletDamage;
            b.TargetLayer = m_isPlayer ? 7 : 6;
            StartCoroutine(Coroutines.DelayBoolChange(false, true, 1f / (m_fireRate / 60f), value => m_canShoot = value));
        }
    }
}