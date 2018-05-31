using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{
    public float MoveSpeed = 250f;
    public WeaponBase[] WeaponsPrefabs;

    [Header("Boundry")] public float BoundaryBuffer = 4;
    public float BoundaryAdjustForce = 70;

    private Damageable _damageable;
    private Rigidbody _rigidBody;
    private LevelBoundary _levelBoundary;

    // Use this for initialization
    void Start()
    {
        _damageable = GetComponent<Damageable>();
        _rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(_damageable);
        Assert.IsNotNull(_rigidBody);
        _levelBoundary = new LevelBoundary(Camera.main);

        foreach (var prefab in WeaponsPrefabs)
        {
            var go = Instantiate(prefab, transform, false);
            //go.name = "Weapon";
            go.GetComponent<WeaponBase>().Ship = _damageable;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = _levelBoundary.Camera.ScreenPointToRay(Input.mousePosition);

        var mousePos = mouseRay.origin;
        mousePos.z = 0;

        var goalDir = mousePos - transform.position;
        goalDir.z = 0;
        goalDir.Normalize();

        transform.rotation = Quaternion.LookRotation(goalDir) * Quaternion.AngleAxis(90, Vector3.up);
    }

    private void FixedUpdate()
    {
        if (_damageable.IsDead)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            AddForce(
                Vector3.left * MoveSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            AddForce(
                Vector3.right * MoveSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            AddForce(
                Vector3.up * MoveSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            AddForce(
                Vector3.down * MoveSpeed);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        KeepPlayerOnScreen();
    }

    public void AddForce(Vector3 force)
    {
        _rigidBody.AddForce(force);
    }

    void KeepPlayerOnScreen()
    {
        var extentLeft = (_levelBoundary.Left + BoundaryBuffer) - transform.position.x;
        var extentRight = transform.position.x - (_levelBoundary.Right - BoundaryBuffer);

        if (extentLeft > 0)
        {
            AddForce(
                Vector3.right * BoundaryAdjustForce * extentLeft);
        }
        else if (extentRight > 0)
        {
            AddForce(
                Vector3.left * BoundaryAdjustForce * extentRight);
        }

        var extentTop = transform.position.y - (_levelBoundary.Top - BoundaryBuffer);
        var extentBottom = (_levelBoundary.Bottom + BoundaryBuffer) - transform.position.y;

        if (extentTop > 0)
        {
            AddForce(
                Vector3.down * BoundaryAdjustForce * extentTop);
        }
        else if (extentBottom > 0)
        {
            AddForce(
                Vector3.up * BoundaryAdjustForce * extentBottom);
        }
    }
}