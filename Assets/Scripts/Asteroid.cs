using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Damageable))]
public class Asteroid : MonoBehaviour
{
    public float MaxSpeed = 10f;
    LevelBoundary _levelBoundary;

    public Rigidbody Rigidbody;
    private Damageable _damageable;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _damageable = GetComponent<Damageable>();
        Assert.IsNotNull(Rigidbody);
        Assert.IsNotNull(_damageable);
    }

    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _damageable = GetComponent<Damageable>();
        Assert.IsNotNull(Rigidbody);
        Assert.IsNotNull(_damageable);
        _levelBoundary = new LevelBoundary(Camera.main);
    }


    public float Scale
    {
        get
        {
            var scale = transform.localScale;
            return scale[0];
        }
        set
        {
            transform.localScale = new Vector3(value, value, value);
            Rigidbody.mass = value;
        }
    }


    private void FixedUpdate()
    {
        var speed = Rigidbody.velocity.magnitude;

        if (speed > MaxSpeed)
        {
            var dir = Rigidbody.velocity / speed;
            Rigidbody.velocity = dir * MaxSpeed;
        }
    }


    private void Update()
    {
        CheckForBoundary();
        transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
    }


    void CheckForBoundary()
    {
        if (transform.position.x > _levelBoundary.Right + Scale && IsMovingInDirection(Vector3.right))
        {
            transform.SetX(_levelBoundary.Left - Scale);
        }
        else if (transform.position.x < _levelBoundary.Left - Scale && IsMovingInDirection(-Vector3.right))
        {
            transform.SetX(_levelBoundary.Right + Scale);
        }
        else if (transform.position.y < _levelBoundary.Bottom - Scale && IsMovingInDirection(-Vector3.up))
        {
            transform.SetY(_levelBoundary.Top + Scale);
        }
        else if (transform.position.y > _levelBoundary.Top + Scale && IsMovingInDirection(Vector3.up))
        {
            transform.SetY(_levelBoundary.Bottom - Scale);
        }
    }

    bool IsMovingInDirection(Vector3 dir)
    {
        return Vector3.Dot(dir, Rigidbody.velocity) > 0;
    }
}