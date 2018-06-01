using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SimpleWeapon : WeaponBase
{
    public GameObject Bullet;

    private void Start()
    {
        Assert.IsNotNull(Bullet);
    }

    public override bool Shoot()
    {
        if (!base.Shoot())
            return false;

        var go = Instantiate(Bullet);

        go.transform.position = Ship.transform.position;
        go.transform.rotation = Ship.transform.rotation;
        var bullet = go.GetComponent<BulletBase>();
        bullet.Ship = Ship.gameObject.GetComponent<Ship>();

        return true;
    }
}