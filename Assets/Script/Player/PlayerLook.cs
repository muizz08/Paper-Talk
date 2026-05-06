using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperTalk.Player
{
    public class PlayerLook : MonoBehaviour
    {
        public Camera _playerCamera;
        private float _xRotation = 0f;
        public float _xSensitivity = 30f;
        public float _ySensitivity = 30f;

        public void ProcessLook(Vector2 input)
        {
        float mouseX = input.x;
        float mouseY = input.y;
        // Calculate the rotation around the x-axis (looking up and down)
        _xRotation -= (mouseY * Time.deltaTime) * _ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
        //apply this to our camera transform
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        //rotate player to look left and right 
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * _xSensitivity);
        }
    }
}
