using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletVelocity;
    private float _gunCooldown = 100; // not an actual cooldown, handles the fire rate mechanic #TODO: rename
    private PlayerLogic _playerLogic;
    private BulletLogic _bulletLogic;
    private PlayerInput _input;
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
    }

    private void UpdatePlayer()
    {
        _playerLogic.UpdatePosition(Time.deltaTime, _input.Player.Accelerate.ReadValue<float>(), _player);
        _playerLogic.Rotate(Time.deltaTime, _input.Player.Rotate.ReadValue<float>(), _rotationSpeed, _player);
        _playerLogic.Shoot(_input.Player.Shoot.ReadValue<float>());
    }

    private void UpdateBullet()
    {
        _gunCooldown += Time.deltaTime;
        if (_input.Player.Shoot.ReadValue<float>() > 0 && _gunCooldown >= 1f / _fireRate)
        {
            GameObject bullet = Instantiate(_bullet, _bullet.transform.position, _player.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.SetActive(true);
            _bulletLogic.AddBullet(bullet, _player.transform.forward, _bulletLifeTime);
            _gunCooldown = 0;
        }
        _bulletLogic.Update(Time.deltaTime, _bulletLifeTime);
    }

    public void ToggleMenu()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        _menu.SetActive(_isPaused);
    }
}
