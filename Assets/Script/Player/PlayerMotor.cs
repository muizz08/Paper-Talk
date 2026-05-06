using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperTalk.Player
{
    public class PlayerMotor : MonoBehaviour
    {
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        public bool _isGrounded;
        public float _Speed = 5f;
        public float _gravity = -9.8f;
        public float _jumpHeight = 3f;
    
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            _isGrounded = _controller.isGrounded;
        }

        public void ProcessMove(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            _controller.Move(transform.TransformDirection(moveDirection) * _Speed * Time.deltaTime);
            _playerVelocity.y += _gravity * Time.deltaTime;
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (_isGrounded)
            {
                _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }
    }

}
