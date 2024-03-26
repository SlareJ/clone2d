using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedRock : Rock
{
    public MedRock(GameObject rock) : base(rock)
    {
        _velocity = 0.9f;
        _rotationSpeed = 90;
        _collider = new Collider(0.3f, 0.3f, -0.3f, -0.3f);
        size = RockSize.Medium;
    }

}
