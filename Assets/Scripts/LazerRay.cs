using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(LineRenderer))]
public class LazerRay : BulletBase
{
    public LineRenderer LineRenderer;
    public float ReyLenght = 70f;
    public float LifeTime = 0.3f;
    private float _endTime;
    
    RaycastHit[] hits;

    void Start()
    {
        Assert.IsNotNull(LineRenderer);
        _endTime = Time.realtimeSinceStartup + LifeTime;
        SetPosition();
    }

    void Update()
    {
        if (Time.realtimeSinceStartup >= _endTime)
        {
            Destroy(gameObject);
            return;
        }

        SetPosition();
        CheckDamage();
    }

    void SetPosition()
    {
        LineRenderer.SetPosition(0, Ship.transform.position + Ship.transform.right);
        LineRenderer.SetPosition(1, Ship.transform.position + Ship.transform.right * ReyLenght * -1);
    }
    


    void CheckDamage()
    {
        hits = Physics.RaycastAll(Ship.transform.position, Ship.transform.right * -1, ReyLenght);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Damageable damageable = hit.transform.GetComponent<Damageable>();

            if (damageable)
            {
                damageable.TakeDamage(Damage);
            }
        }
    }
}