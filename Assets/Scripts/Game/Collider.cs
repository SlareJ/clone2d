using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider
{
    public float upperBound;
    public float lowerBound;
    public float leftBound;
    public float rightBound;

    public Collider(float upper, float right, float lower, float left)
    {
        upperBound = upper;
        rightBound = right;
        lowerBound = lower;
        leftBound = left;
    }
}
