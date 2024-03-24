using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _largeRock;
    [SerializeField] private GameObject _medRock;
    [SerializeField] private GameObject _smallRock;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletVelocity;
    private PlayerLogic _playerLogic;
    private BulletLogic _bulletLogic;
    private RockLogic _rockLogic;
    private PlayerInput _input;
    private float _timeLastShot = 100;
    private int _rocksAmount = 0;
    private int _currentLevel = 0;
    private bool _isPaused = false;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.ToggleMenu.performed += ctx => ToggleMenu();
    }

    private void Start()
    {
        _playerLogic = new PlayerLogic(_acceleration, _maxVelocity);
        _bulletLogic = new BulletLogic(_bulletVelocity);
        _rockLogic = new RockLogic();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        UpdatePlayer();
        UpdateBullet();
        UpdateRocks();
    }

    private void UpdatePlayer()
    {
        _playerLogic.UpdatePosition(Time.deltaTime, _input.Player.Accelerate.ReadValue<float>(), _player);
        _playerLogic.Rotate(Time.deltaTime, _input.Player.Rotate.ReadValue<float>(), _rotationSpeed, _player);
    }

    private void UpdateBullet()
    {
        _timeLastShot += Time.deltaTime;
        if (_input.Player.Shoot.ReadValue<float>() > 0 && _timeLastShot >= 1f / _fireRate)
        {
            GameObject bullet = Instantiate(_bullet, _bullet.transform.position, _player.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.SetActive(true);
            _bulletLogic.AddBullet(bullet, _player.transform.forward, _bulletLifeTime);
            _timeLastShot = 0;
        }
        _bulletLogic.Update(Time.deltaTime, _bulletLifeTime);
    }

    private void UpdateRocks()
    {
        SpawnRocks();
        _rockLogic.Update(Time.deltaTime);
    }
    private void SpawnRocks()
    {
        if (_rocksAmount == 0)
        {
            ++_currentLevel;
            _rocksAmount = _currentLevel * 2 + 3;
            for (int i = 0; i < _rocksAmount; ++i)
            {
                Vector2 randCoords;
                do
                {
                    randCoords.x = UnityEngine.Random.Range(0f, 1f);
                    randCoords.y = UnityEngine.Random.Range(0f, 1f);
                    randCoords = Camera.main.ViewportToWorldPoint(new Vector3(MathF.Abs(randCoords.x), MathF.Abs(randCoords.y), 0));
                }
                while (Vector3.Distance(randCoords, _player.transform.position) < 4);
                GameObject rock = Instantiate(_largeRock, randCoords, _largeRock.transform.rotation);
                rock.SetActive(true);
                _rockLogic.SpawnLargeRock(rock);
            }
        }
    }

    public void ToggleMenu()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        _menu.SetActive(_isPaused);
    }
}
