using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int TargetLayer { get; set; }
    public Rigidbody Rb { get { return GetComponent<Rigidbody>(); }}
    public float FlightTime {  get; set; }
    public float Damage {  get; set; }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == TargetLayer)
        {
            collision.gameObject.GetComponentInParent<Health>().TakeDamage(Damage);
        }
    }
}