using System.Collections.Generic;
using UnityEngine;

public class RockLogic
{
    private List<Rock> _rocks = new List<Rock>();

    public RockLogic()
    {
        CollisionManager.Instance.SetRocks(_rocks);
    }

    public void SpawnLargeRock(GameObject rock)
    {
        LargeRock newRock = new LargeRock(rock);
        _rocks.Add(newRock);
    }

    public void SpawnMedRock(GameObject rock)
    {
        MedRock newRock = new MedRock(rock);
        _rocks.Add(newRock);
    }

    public void SpawnSmallRock(GameObject rock)
    {
        SmallRock newRock = new SmallRock(rock);
        _rocks.Add(newRock);
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < _rocks.Count; i++)
        {
            _rocks[i].Update(deltaTime);
        }
    }
}
