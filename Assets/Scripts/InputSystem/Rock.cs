using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rock
{
    private GameObject _rock;
    private Vector3 _direction;
    protected float _velocity = 1;
    protected float _rotationSpeed = 1;

    public Rock() { }

    public Rock(GameObject rock)
    {
        _rock = rock;
        _direction = Random.insideUnitCircle.normalized;
    }

    public void Update(float deltaTime)
    {
        _rock.transform.position += _direction * _velocity * deltaTime;
        _rock.transform.Rotate(0, _rotationSpeed * deltaTime, 0);
        ScreenWrap();
    }

    public void ScreenWrap()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(_rock.transform.position);

        if (viewportPosition.x < 0) viewportPosition.x = 1;
        if (viewportPosition.x > 1) viewportPosition.x = 0;
        if (viewportPosition.y < 0) viewportPosition.y = 1;
        if (viewportPosition.y > 1) viewportPosition.y = 0;

        _rock.transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }

    public void Destroy()
    {
        GameObject.Destroy(_rock);
    }
}
