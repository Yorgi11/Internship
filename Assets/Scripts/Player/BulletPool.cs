using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton_template<BulletPool>
{
    private readonly Queue<Bullet> m_bullets = new();
    private readonly HashSet<Bullet> m_activeBullets = new();

    [SerializeField] private float m_flightTime;
    [SerializeField] private int m_initialCount;
    [SerializeField] private Bullet m_bulletPrefab;
    [SerializeField] private Transform m_parentTransform;
    private void Update()
    {
        if (m_bullets.Count <= m_initialCount)
        {
            AddBulletToPool();
        }
        List<Bullet> tempList = new();
        foreach (Bullet bullet in m_activeBullets)
        {
            if(Time.realtimeSinceStartup - bullet.FlightTime >= m_flightTime) tempList.Add(bullet);
        }
        foreach (Bullet bullet in tempList)
        {
            ReturnBullet(bullet);
        }
    }
    private Bullet AddBulletToPool()
    {
        Bullet bullet = Object.Instantiate(m_bulletPrefab, m_parentTransform);
        bullet.gameObject.SetActive(false); // Initially deactivate
        m_bullets.Enqueue(bullet);
        return bullet;
    }
    public Bullet GetBullet()
    {
        if (m_bullets.Count == 0)
        {
            AddBulletToPool();
        }
        var bullet = m_bullets.Dequeue();
        bullet.gameObject.SetActive(true);
        m_activeBullets.Add(bullet);
        return bullet;
    }
    public Bullet GetBullet(Vector3 pos, Quaternion rot)
    {
        if (m_bullets.Count == 0)
        {
            AddBulletToPool();
        }
        var bullet = m_bullets.Dequeue();
        bullet.gameObject.SetActive(true);
        bullet.transform.SetPositionAndRotation(pos, rot);
        bullet.FlightTime = Time.realtimeSinceStartup;
        m_activeBullets.Add(bullet);
        return bullet;
    }
    public void ReturnBullet(Bullet bullet)
    {
        ResetBullet(bullet);
        bullet.gameObject.SetActive(false);
        m_bullets.Enqueue(bullet);
        m_activeBullets.Remove(bullet);
    }
    private void ResetBullet(Bullet bullet)
    {
        bullet.Rb.position = m_parentTransform.position;
        bullet.Rb.velocity = Vector3.zero;
        bullet.Rb.angularVelocity = Vector3.zero;
    }
}