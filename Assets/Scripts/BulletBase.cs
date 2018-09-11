using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour, ICanDamage
{
    public Ship Ship;
    public int Damage = 1;
}