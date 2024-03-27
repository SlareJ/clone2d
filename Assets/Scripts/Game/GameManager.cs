using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _largeRock;
    [SerializeField] private GameObject _medRock;
    [SerializeField] private GameObject _smallRock;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _speedText;
    [SerializeField] private float _playerAcceleration;
    [SerializeField] private float _playerMaxVelocity;
    [SerializeField] private float _playerRotationSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float _playerFireRate;
    [SerializeField] private float _bulletVelocity;
    private PlayerLogic _playerLogic;
    private BulletLogic _bulletLogic;
    private RockLogic _rockLogic;
    private PlayerInput _input;
    private CollisionManager _collisionManager = CollisionManager.Instance;
    private float _timeLastShot = 100;
    private int _rocksAmount = 0;
    private int _currentLevel = 0;
    private int _score = 0;
    private bool _isPaused = false;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.ToggleMenu.performed += ctx => ToggleMenu();
    }

    private void Start()
    {
        _playerLogic = new PlayerLogic(_playerAcceleration, _playerMaxVelocity);
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
        CheckPlayerCollisions();
        CheckBulletCollisions();
        UpdateHUD();
    }

    private void UpdatePlayer()
    {
        _playerLogic.UpdatePosition(Time.deltaTime, _input.Player.Accelerate.ReadValue<float>(), _player);
        _playerLogic.Rotate(Time.deltaTime, _input.Player.Rotate.ReadValue<float>(), _playerRotationSpeed, _player);
    }

    private void UpdateBullet()
    {
        _timeLastShot += Time.deltaTime;
        if (_input.Player.Shoot.ReadValue<float>() > 0 && _timeLastShot >= 1f / _playerFireRate)
        {
            GameObject bullet = Instantiate(_bullet, _bullet.transform.position, _player.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.SetActive(true);
            _bulletLogic.SpawnBullet(bullet, _player.transform.forward, _bulletLifeTime, _bulletVelocity);
            _timeLastShot = 0;
        }
        _bulletLogic.Update(Time.deltaTime);
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
            _rocksAmount = _currentLevel * 15 / 10 + 2;
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

    private void CheckPlayerCollisions()
    {
        if (_collisionManager.CheckPlayerCollisions(GetPlayerPosition(), _playerLogic.GetPlayerCollider()))
        {
            _gameOver.SetActive(true);
            OnDisable();
        }
    }

    private void CheckBulletCollisions()
    {
        Tuple<Vector3, RockSize> temp = _collisionManager.CheckBulletCollisions();
        Vector3 pos = temp.Item1;
        RockSize size = temp.Item2;

        if (size == RockSize.Large)
        {
            _rocksAmount += 1;
            GameObject newRock0 = Instantiate(_medRock, pos, _medRock.transform.rotation);
            newRock0.SetActive(true);
            _rockLogic.SpawnMedRock(newRock0);
            GameObject newRock1 = Instantiate(_medRock, pos, _medRock.transform.rotation);
            newRock1.SetActive(true);
            _rockLogic.SpawnMedRock(newRock1);
            _score += 20;
        }
        else if (size == RockSize.Medium)
        {
            _rocksAmount += 1;
            GameObject newRock0 = Instantiate(_smallRock, pos, _smallRock.transform.rotation);
            newRock0.SetActive(true);
            _rockLogic.SpawnSmallRock(newRock0);
            GameObject newRock1 = Instantiate(_smallRock, pos, _smallRock.transform.rotation);
            newRock1.SetActive(true);
            _rockLogic.SpawnSmallRock(newRock1);
            _score += 10;
        }
        else if (size == RockSize.Small && pos != Vector3.zero)
        {
            _rocksAmount -= 1;
            _score += 5;
        }
    }

    private void UpdateHUD()
    {
        _scoreText.text = "Score: " + _score;
        _levelText.text = "Level: " + _currentLevel;
        _speedText.text = "Speed: " + (_playerLogic.GetPlayerVelocity() * 10).ToString("F0") + "/100";
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }

    public void ToggleMenu()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        _menu.SetActive(_isPaused);
    }
}
