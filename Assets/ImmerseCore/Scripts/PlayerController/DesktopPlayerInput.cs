using UnityEngine;

namespace Immerse.Core
{
    public class DesktopPlayerInput : MonoBehaviour
    {
#if UNITY_EDITOR
        private Vector2 _movementInput = Vector2.zero;
        private Vector2 _cursorInput = Vector2.zero;

        public bool IsMoving => _movementInput != Vector2.zero;
        public Vector2 PlayerMovement => _movementInput;

        public bool IsRotating => Input.GetKey(KeyCode.LeftControl);
        public Vector2 Cursor => _cursorInput;


        private void Update()
        {
            _movementInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
            _cursorInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
#endif
    }
}