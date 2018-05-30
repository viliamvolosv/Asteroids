using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary
{
    public readonly Camera Camera;

    public LevelBoundary(Camera camera)
    {
        Camera = camera;
    }

    public float Bottom
    {
        get { return -ExtentHeight; }
    }

    public float Top
    {
        get { return ExtentHeight; }
    }

    public float Left
    {
        get { return -ExtentWidth; }
    }

    public float Right
    {
        get { return ExtentWidth; }
    }

    public float ExtentHeight
    {
        get { return Camera.orthographicSize; }
    }

    public float Height
    {
        get { return ExtentHeight * 2.0f; }
    }

    public float ExtentWidth
    {
        get { return Camera.aspect * Camera.orthographicSize; }
    }

    public float Width
    {
        get { return ExtentWidth * 2.0f; }
    }
}