﻿using UnityEngine;


public class Rotator : MonoBehaviour
{
    [Range(-1.0f, 1.0f)]
    public float xForceDirection = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float yForceDirection = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float zForceDirection = 0.0f;

    public float speedMultiplier = 1;

    public bool worldPivote = false;

    private Space spacePivot = Space.Self;

    void Start()
    {
        if (worldPivote) spacePivot = Space.World;
    }

    void FixedUpdate()
    {
        this.transform.Rotate(xForceDirection * speedMultiplier * 5, yForceDirection * speedMultiplier * 5, zForceDirection * speedMultiplier * 5, spacePivot);
    }
}