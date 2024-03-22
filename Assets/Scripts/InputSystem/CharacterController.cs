using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private PlayerInput _input;
    private bool _isPaused = false;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.Shoot.performed += ctx => Shoot();
        _input.Player.ToggleMenu.performed += ctx => ToggleMenu();
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
        Move(_input.Player.Move.ReadValue<Vector2>());
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }

    private void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * _moveSpeed * Time.deltaTime;
    }

    private void ToggleMenu()
    {

    }

}
