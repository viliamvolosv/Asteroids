using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class WeaponBase : MonoBehaviour
{
    public float Couldown = 1f;
    public KeyCode Key = KeyCode.Space;
    private float _timeToNextShot = 0;
    public Damageable Ship;

    public virtual bool Shoot()
    {
        if (!IsCanShoot())
            return false;
        _timeToNextShot = Couldown;
        return true;
    }

    public virtual bool IsCanShoot()
    {
        
        return Ship!= null && !Ship.IsDead && _timeToNextShot <= 0;
    }

    protected virtual void Update()
    {
        if(Ship == null)
            return;
        
        if (_timeToNextShot > 0)
        {
            _timeToNextShot -= Time.deltaTime;
        }

        if (Input.GetKey(Key))
        {
            Shoot();
        }
    }
}