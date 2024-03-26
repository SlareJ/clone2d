using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class CollisionManager
{
    private static CollisionManager _instance;
    public static CollisionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CollisionManager();
            }
            return _instance;
        }
    }

    private List<Rock> _rocks;
    private List<Bullet> _bullets;

    public bool CheckPlayerCollisions(Vector3 playerPos, Collider playerCollider)
    {
        for (int i = 0; i < _rocks.Count; i++)
        {
            if (CheckCollision(playerPos, _rocks[i].GetRockPosition(), playerCollider, _rocks[i].GetRockCollider()))
            {
                return true;
            }
        }
        return false;
    }

    public Tuple<Vector3, RockSize> CheckBulletCollisions()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            for (int j = 0; j < _rocks.Count; j++)
            {
                if (CheckCollision(_bullets[i].GetBulletPosition(), _rocks[j].GetRockPosition(),
                _bullets[i].GetBulletCollider(), _rocks[j].GetRockCollider()))
                {
                    _bullets[i].Destroy();
                    _bullets.RemoveAt(i);
                    Vector3 rockPosition = _rocks[j].GetRockPosition();
                    RockSize size = _rocks[j].size;
                    _rocks[j].Destroy();
                    _rocks.RemoveAt(j);
                    return new Tuple<Vector3, RockSize>(rockPosition, size);
                }
            }
        }
        return new Tuple<Vector3, RockSize>(Vector3.zero, RockSize.Small);
    }

    private bool CheckCollision(Vector3 playerPos, Vector3 rockPos, Collider playerCollider, Collider rockCollider)
    {
        if (playerPos.x + playerCollider.rightBound > rockPos.x + rockCollider.leftBound &&
            playerPos.x + playerCollider.leftBound < rockPos.x + rockCollider.rightBound &&
            playerPos.y + playerCollider.upperBound > rockPos.y + rockCollider.lowerBound &&
            playerPos.y + playerCollider.lowerBound < rockPos.y + rockCollider.upperBound)
        {
            return true;
        }
        return false;
    }

    public void SetRocks(List<Rock> rocks)
    {
        _rocks = rocks;
    }

    public void SetBullets(List<Bullet> bullets)
    {
        _bullets = bullets;
    }
}
