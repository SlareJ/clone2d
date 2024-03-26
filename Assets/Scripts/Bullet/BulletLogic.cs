using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic
{
    private float _velocity;
    private List<Bullet> _bullets = new List<Bullet>();

    public BulletLogic(float velocity)
    {
        _velocity = velocity;
        CollisionManager.Instance.SetBullets(_bullets);
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].IsAlive())
            {
                _bullets[i].Destroy();
                _bullets.RemoveAt(i);
                continue;
            }
            _bullets[i].Update(deltaTime);
        }
    }

    public void SpawnBullet(GameObject bullet, Vector3 direction, float lifeTime, float velocity)
    {
        Bullet newBullet = new Bullet(bullet, direction, lifeTime, velocity);
        _bullets.Add(newBullet);
    }
}
