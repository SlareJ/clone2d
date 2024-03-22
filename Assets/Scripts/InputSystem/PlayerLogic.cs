using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic
{
    private float _acceleration = 0;
    private float _maxVelocity = 0;
    private Vector3 _velocity;
    private GameObject _player;

    public PlayerLogic(float acceleration, float maxVelocity, GameObject player)
    {
        _acceleration = acceleration;
        _maxVelocity = maxVelocity;
        _player = player;
    }

    public void Accelerate(Vector3 forward, float deltaTime)
    {
        _velocity += forward * _acceleration * deltaTime;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);
    }

    public void UpdatePosition(float deltaTime, float isAccelerating, Vector3 forwardVector)
    {
        if (isAccelerating > 0) Accelerate(forwardVector, deltaTime);
        _player.transform.position += _velocity * deltaTime;
    }

    public void Rotate(float deltaTime, float rotationSide, float rotationSpeed)
    {
        _player.transform.Rotate(0, rotationSide * rotationSpeed * deltaTime, 0);
    }
}
