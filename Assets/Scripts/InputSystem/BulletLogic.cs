using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletLogic
{
    private class BulletData
    {
        public GameObject bullet;
        public Vector3 direction;
        public float lifeTime;

        public BulletData(GameObject bullet, Vector3 direction, float lifeTime)
        {
            this.bullet = bullet;
            this.direction = direction;
            this.lifeTime = lifeTime;
        }

    }
    private float _velocity;
    private List<BulletData> _bullets = new List<BulletData>();

    public BulletLogic(float velocity)
    {
        _velocity = velocity;
    }

    public void Update(float deltaTime, float bulletLifeTime)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {

            _bullets[i].bullet.transform.position += _bullets[i].direction * _velocity * deltaTime;
            _bullets[i].lifeTime -= deltaTime;
            if (_bullets[i].lifeTime <= 0)
            {
                GameObject.Destroy(_bullets[i].bullet);
                _bullets.RemoveAt(i);
            }
            ScreenWrap(_bullets[i].bullet);
        }
    }

    public void AddBullet(GameObject bullet, Vector3 direction, float lifeTime)
    {
        _bullets.Add(new BulletData(bullet, direction, lifeTime));
    }

    private void ScreenWrap(GameObject bullet)
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(bullet.transform.position);

        if (viewportPosition.x < 0) viewportPosition.x = 1;
        if (viewportPosition.x > 1) viewportPosition.x = 0;
        if (viewportPosition.y < 0) viewportPosition.y = 1;
        if (viewportPosition.y > 1) viewportPosition.y = 0;

        bullet.transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }
}
