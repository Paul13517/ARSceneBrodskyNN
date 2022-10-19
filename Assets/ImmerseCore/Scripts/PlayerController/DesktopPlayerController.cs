using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Immerse.Core
{
    public class DesktopPlayerController : MonoBehaviour
    {
#if UNITY_EDITOR
        [Space, SerializeField] private float _eyeHeight = 1.7f;
        [SerializeField] private float _movementSpeed = 4f;
        [SerializeField] private float _rotationSpeed = 2.0f;

        private Camera _playerCamera;
        private DesktopPlayerInput _playerInput;


        private void Awake()
        {
            _playerInput = GetComponentInChildren<DesktopPlayerInput>();
            _playerCamera = GetComponentInChildren<Camera>();

            _playerCamera.transform.localPosition = _eyeHeight * Vector3.up;
        }

        private void Update()
        {
            UpdateMovement();
            UpdateCamera();
        }

        private void UpdateMovement()
        {
            if (!_playerInput.IsMoving)
            {
                return;
            }

            Vector2 offset = _playerInput.PlayerMovement;

            if (offset.sqrMagnitude > 1f)
            {
                offset = offset.normalized;
            }

            Vector3 positionOffset = new Vector3(offset.y, 0.0f, offset.x);
            var position = Time.deltaTime * _movementSpeed * positionOffset;

            Quaternion horizontalRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.position += horizontalRotation * position;
        }

        private void UpdateCamera()
        {
            if (!_playerInput.IsRotating)
            {
                UnlockCursor();
                return;
            }

            LockCursor();

            var rotation = _playerCamera.transform.rotation.eulerAngles;

            rotation.x -= _playerInput.Cursor.y * _rotationSpeed;
            rotation.y += _playerInput.Cursor.x * _rotationSpeed;

            if (rotation.x >= 270f)
            {
                rotation.x -= 360f;
            }

            rotation.y = Mathf.Repeat(rotation.y, 360);
            rotation.x = Mathf.Clamp(rotation.x, -90, 90);

            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            _playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        }


        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
#endif
    }
}