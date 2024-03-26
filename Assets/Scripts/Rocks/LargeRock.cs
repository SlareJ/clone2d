using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeRock : Rock
{
    public LargeRock(GameObject rock) : base(rock)
    {
        _velocity = 1;
        _rotationSpeed = 10;
        _collider = new Collider(0.5f, 0.5f, -0.5f, -0.5f);
    }

}
