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
    [SerializeField] private GameObject player;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float rotationSpeed;
    private PlayerLogic playerLogic;
    private PlayerInput _input;
    private bool _isPaused = false;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.ToggleMenu.performed += ctx => ToggleMenu();
    }

    private void Start()
    {
        playerLogic = new PlayerLogic(_acceleration, _maxVelocity, player);
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
        playerLogic.UpdatePosition(Time.deltaTime, _input.Player.Accelerate.ReadValue<float>(), player.transform.forward);
        playerLogic.Rotate(Time.deltaTime, _input.Player.Rotate.ReadValue<float>(), rotationSpeed);
    }

    public void ToggleMenu()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        _menu.SetActive(_isPaused);
    }
}
