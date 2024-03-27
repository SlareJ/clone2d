using UnityEngine;

public class Bullet
{
    private GameObject _bullet;
    private Vector3 _direction;
    private Collider _collider = new Collider(0.2f, 0.2f, -0.2f, -0.2f);
    private float _lifeTime;
    private float _velocity;

    public Bullet(GameObject bullet, Vector3 direction, float lifeTime, float velocity)
    {
        _bullet = bullet;
        _direction = direction;
        _lifeTime = lifeTime;
        _velocity = velocity;
    }

    public void Update(float deltaTime)
    {
        _bullet.transform.position += _direction * _velocity * deltaTime;
        _lifeTime -= Time.deltaTime;
        ScreenWrap();
    }

    public bool IsAlive()
    {
        return _lifeTime > 0;
    }

    public void ScreenWrap()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(_bullet.transform.position);

        if (viewportPosition.x < 0) viewportPosition.x = 1;
        if (viewportPosition.x > 1) viewportPosition.x = 0;
        if (viewportPosition.y < 0) viewportPosition.y = 1;
        if (viewportPosition.y > 1) viewportPosition.y = 0;

        _bullet.transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }

    public void Destroy()
    {
        GameObject.Destroy(_bullet);
    }

    public Vector3 GetBulletPosition()
    {
        return _bullet.transform.position;
    }

    public Collider GetBulletCollider()
    {
        return _collider;
    }
}
