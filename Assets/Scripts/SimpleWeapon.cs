using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SimpleWeapon : WeaponBase {
	
	public GameObject Bullet;
	public float BulletOffsetDistance = 2f;

	private void Start()
	{
		Assert.IsNotNull(Bullet);
	}

	public override bool Shoot()
	{
		if (!base.Shoot())
			return false;
		
		Debug.Log("Shoot "+gameObject.name);

		var bullet = Instantiate(Bullet);

		bullet.transform.position = Ship.transform.position + Ship.transform.forward * BulletOffsetDistance;
		bullet.transform.rotation = Ship.transform.rotation;
		return true;
	}
}
