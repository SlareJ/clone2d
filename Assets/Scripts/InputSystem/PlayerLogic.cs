using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic
{

    private float _acceleration = 0;
    private float _maxVelocity;
    private Vector3 _velocity;
    private Vector3 _position;
    public PlayerLogic(float acceleration, float maxVelocity, Vector3 position)
    {
        _position = position;
        _acceleration = acceleration;
        _maxVelocity = maxVelocity;
    }

    public void Accelerate(Vector3 forward, float deltaTime)
    {
        _velocity += forward * _acceleration * deltaTime;
        Debug.Log(forward);
        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);
    }

    public Vector3 UpdatePosition(float deltaTime)
    {
        _position += _velocity * deltaTime;
        return _position;
    }



}
