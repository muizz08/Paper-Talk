using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTalk.InputManager
{
   using PaperTalk.Player;
    public class InputManager : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private PlayerInput.OnFootActions _onFoot;
        [SerializeField] private PlayerMotor _motor;
        [SerializeField] private PlayerLook _look;
        [SerializeField] private PlayerInteract _interact;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _onFoot = _playerInput.OnFoot;
            _onFoot.Jump.performed += ctx => _motor.Jump();
            _onFoot.Pickup.performed += ctx => _interact.OnPickup();
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            _motor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
        }

        private void LateUpdate()
        {
            _look.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _onFoot.Enable();
        }

        private void OnDisable()
        {
            _onFoot.Disable();
        }   
    }
}
