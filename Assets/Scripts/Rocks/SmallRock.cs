using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : Rock
{
    public SmallRock(GameObject rock) : base(rock)
    {
        _velocity = 10;
        _rotationSpeed = 120;
    }
}
