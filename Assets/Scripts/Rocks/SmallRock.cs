using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : Rock
{
    public SmallRock(GameObject rock) : base(rock)
    {
        _velocity = 1.5f;
        _rotationSpeed = 120;
        _collider = new Collider(0.15f, 0.15f, -0.15f, -0.15f);
        size = RockSize.Small;
    }
}
