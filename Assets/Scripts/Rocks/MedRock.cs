using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedRock : Rock
{
    public MedRock(GameObject rock) : base(rock)
    {
        _velocity = 7;
        _rotationSpeed = 90;
    }

}
