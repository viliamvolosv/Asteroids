using System;
using System.Collections;
using System.Collections.Generic;
using Core.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class AsteroidsManager : MonoBehaviour
{
    [Header("Asteroids")] public Asteroid AsteroidPrefab;
    public Vector2 ScaleRange;
    public Vector2 MassRange;
    public int AsteroidsCount = 4;
    public float InitialSpeed = 10f;

    List<Asteroid> _asteroids = new List<Asteroid>();
    LevelBoundary _levelBoundary;


    public void Start()
    {
        Assert.IsNotNull(AsteroidPrefab);

        _levelBoundary = new LevelBoundary(Camera.main);
        for (int i = 0; i < AsteroidsCount; i++)
        {
            SpawnNext();
        }
    }

    public void SpawnNext()
    {
        var go = Instantiate(AsteroidPrefab);
        var asteroid = go.GetComponent<Asteroid>();

        asteroid.Scale = Mathf.Lerp(ScaleRange.x, ScaleRange.y, UnityEngine.Random.value);
        asteroid.Rigidbody.mass = Mathf.Lerp(ScaleRange.x, ScaleRange.y, UnityEngine.Random.value);
        asteroid.transform.position = GetRandomStartPosition(asteroid.Scale);
        asteroid.Rigidbody.velocity = GetRandomDirection() * InitialSpeed;

        _asteroids.Add(asteroid);
        asteroid.GetComponent<Damageable>().OnDiedAction += OnDiedAction;
    }

    private void OnDiedAction(Damageable damageable)
    {
        damageable.OnDiedAction -= OnDiedAction;
        var asteroid = damageable.GetComponent<Asteroid>();
        _asteroids.Remove(asteroid);
        SpawnNext();
    }

    Vector3 GetRandomDirection()
    {
        var theta = Random.Range(0, Mathf.PI * 2.0f);
        return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
    }

    Vector3 GetRandomStartPosition(float scale)
    {
        var side = (Side) Random.Range(0, (int) Side.Count);
        var rand = Random.Range(0.0f, 1.0f);

        switch (side)
        {
            case Side.Top:
            {
                return new Vector3(_levelBoundary.Left + rand * _levelBoundary.Width, _levelBoundary.Top + scale, 0);
            }
            case Side.Bottom:
            {
                return new Vector3(_levelBoundary.Left + rand * _levelBoundary.Width, _levelBoundary.Bottom - scale, 0);
            }
            case Side.Right:
            {
                return new Vector3(_levelBoundary.Right + scale, _levelBoundary.Bottom + rand * _levelBoundary.Height,
                    0);
            }
            case Side.Left:
            {
                return new Vector3(_levelBoundary.Left - scale, _levelBoundary.Bottom + rand * _levelBoundary.Height,
                    0);
            }
        }

        return Vector3.zero;
    }

    enum Side
    {
        Top,
        Bottom,
        Left,
        Right,
        Count
    }
}