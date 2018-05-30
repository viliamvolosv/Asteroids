using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public int StartHealth = 10;
    public bool DestroyWhenDie = false;
    private int _health;
    private bool isDead = false;
    public UnityAction<Damageable> OnDiedAction;
    public UnityAction<Damageable> OnHealthChangeAction;

    public int Health
    {
        get { return _health; }
        private set
        {
            if (_health == value)
                return;
            _health = value;
            if (_health < 0)
            {
                _health = 0;
            }

            if (_health == 0)
            {
                if (!isDead && OnDiedAction != null)
                    OnDiedAction(this);

                isDead = true;
            }

            if (OnHealthChangeAction != null)
                OnHealthChangeAction(this);
            if(DestroyWhenDie && isDead)
                Destroy(this.gameObject);
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    private void Start()
    {
        Assert.Greater(StartHealth, 0);
        Health = StartHealth;
    }
    
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
    }
}