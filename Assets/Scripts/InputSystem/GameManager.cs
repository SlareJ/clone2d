using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Player player;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxVelocity;
    private PlayerLogic playerLogic;
    private PlayerInput _input;
    private bool _isPaused = false;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.ToggleMenu.performed += ctx => ToggleMenu();
        _input.Player.Shoot.performed += ctx => player.Shoot();
    }

    private void Start()
    {
        playerLogic = new PlayerLogic(_acceleration, _maxVelocity, player.transform.position);
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
        if (_input.Player.Accelerate.ReadValue<float>() > 0) AccelerateShip();
        player.transform.position = playerLogic.UpdatePosition(Time.deltaTime);
    }

    private void AccelerateShip()
    {
        playerLogic.Accelerate(Quaternion.Euler(-90, 0, 0) * player.transform.forward, Time.deltaTime);
    }


    public void ToggleMenu()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        _menu.SetActive(_isPaused);
    }
}
