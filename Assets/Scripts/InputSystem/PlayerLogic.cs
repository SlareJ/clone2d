using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic
{
    private float _acceleration = 0;
    private float _maxVelocity = 0;
    private Vector3 _velocity;

    public PlayerLogic(float acceleration, float maxVelocity)
    {
        _acceleration = acceleration;
        _maxVelocity = maxVelocity;
    }

    public void Accelerate(Vector3 forward, float deltaTime)
    {
        _velocity += forward * _acceleration * deltaTime;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);
    }

    public void UpdatePosition(float deltaTime, float isAccelerating, GameObject player)
    {
        if (isAccelerating > 0) Accelerate(player.transform.forward, deltaTime);
        player.transform.position += _velocity * deltaTime;
        ScreenWrap(player);
    }

    public void Rotate(float deltaTime, float rotationSide, float rotationSpeed, GameObject player)
    {
        player.transform.Rotate(0, rotationSide * rotationSpeed * deltaTime, 0);
    }

    public void ScreenWrap(GameObject player)
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(player.transform.position);

        if (viewportPosition.x < 0) viewportPosition.x = 1;
        if (viewportPosition.x > 1) viewportPosition.x = 0;
        if (viewportPosition.y < 0) viewportPosition.y = 1;
        if (viewportPosition.y > 1) viewportPosition.y = 0;

        player.transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }
}
