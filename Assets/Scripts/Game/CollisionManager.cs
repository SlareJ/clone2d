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

    public bool Update(Vector3 playerPos, Collider playerCollider)
    {
        for (int i = 0; i < _rocks.Count; i++)
        {
            if (CheckCollisions(playerPos, _rocks[i].GetRockPosition(), playerCollider, _rocks[i].GetRockCollider()))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckCollisions(Vector3 playerPos, Vector3 rockPos, Collider playerCollider, Collider rockCollider)
    {
        if (playerPos.x + playerCollider.rightBound > rockPos.x + rockCollider.leftBound &&
            playerPos.x + playerCollider.leftBound < rockPos.x + rockCollider.rightBound &&
            playerPos.y + playerCollider.upperBound > rockPos.y + rockCollider.lowerBound &&
            playerPos.y + playerCollider.lowerBound < rockPos.y + rockCollider.upperBound)
        {
            Debug.Log("Collided");
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
