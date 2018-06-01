using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    public float Speed;
    public float LifeTime;
    float _startTime;

    private void Start()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    public void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Damageable>();
        var ship = other.GetComponent<Ship>();
        if (ship != null)
            return;

        if (damageable != null)
        {
            damageable.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        transform.position -= transform.right * Speed * Time.deltaTime;

        if (Time.realtimeSinceStartup - _startTime > LifeTime)
        {
            Destroy(gameObject);
        }
    }
}